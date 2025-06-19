using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using Discord.Net.Rest;

namespace KingmakerDiscordBot.Application.Observability;

internal sealed class TracedRestClient(string baseUrl, HttpClient client) : IRestClient
{
    private static readonly Func<object, string?> ContentTypePropertyReader;
    private static readonly Func<object, string> FileNamePropertyReader;
    private static readonly Func<object, Stream> StreamPropertyReader;
    private static readonly Type MultipartFileType;
    private readonly Uri _baseUri = new(baseUrl);
    private CancellationToken _cancellationToken;

    static TracedRestClient()
    {
        const string typeName = "Discord.Net.Rest.MultipartFile";

        MultipartFileType = typeof(DefaultRestClientProvider)
            .Assembly
            .GetType(typeName) ?? throw new InvalidOperationException($"Could not find {typeName}");

        StreamPropertyReader = GetProperty<Stream>("Stream");
        FileNamePropertyReader = GetProperty<string>("Filename");
        ContentTypePropertyReader = GetProperty<string?>("ContentType");

        return;

        Func<object, T> GetProperty<T>(string propertyName)
        {
            var property = MultipartFileType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance) ??
                           throw new InvalidOperationException($"Could not find property {propertyName} on {typeName}");

            var parameter = Expression.Parameter(typeof(object), "instance");
            var incomingCast = Expression.Convert(parameter, MultipartFileType);
            var propertyExpression = Expression.Property(incomingCast, property);

            return Expression
                .Lambda<Func<object, T>>(propertyExpression, parameter)
                .Compile();
        }
    }

    public void Dispose()
    {
    }

    public void SetHeader(string key, string? value)
    {
        client.DefaultRequestHeaders.Remove(key);

        if (value is not null)
        {
            client.DefaultRequestHeaders.Add(key, value);
        }
    }

    public void SetCancelToken(CancellationToken cancelToken)
    {
        _cancellationToken = cancelToken;
    }

    public Task<RestResponse> SendAsync(string method, string endpoint, CancellationToken cancellationToken,
        bool headerOnly, string? reason = null,
        IEnumerable<KeyValuePair<string, IEnumerable<string>>>? requestHeaders = null)
    {
        return SendInternalAsync(method, endpoint, headerOnly, reason, null, requestHeaders, cancellationToken);
    }

    public Task<RestResponse> SendAsync(string method, string endpoint, string json,
        CancellationToken cancellationToken, bool headerOnly, string? reason = null,
        IEnumerable<KeyValuePair<string, IEnumerable<string>>>? requestHeaders = null)
    {
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return SendInternalAsync(method, endpoint, headerOnly, reason, content, requestHeaders, cancellationToken);
    }

    public Task<RestResponse> SendAsync(string method, string endpoint,
        IReadOnlyDictionary<string, object>? multipartParameters, CancellationToken cancellationToken, bool headerOnly,
        string? reason = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>>? requestHeaders = null)
    {
        var content = CreateMultipartContent(multipartParameters);

        return SendInternalAsync(method, endpoint, headerOnly, reason, content, requestHeaders, cancellationToken);
    }

    private async Task<RestResponse> SendInternalAsync(string method, string endpoint, bool headerOnly, string? reason,
        HttpContent? content, IEnumerable<KeyValuePair<string, IEnumerable<string>>>? requestHeaders,
        CancellationToken cancellationToken)
    {
        using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(_cancellationToken,
            cancellationToken);

        var uri = new Uri(_baseUri, endpoint);
        var httpMethod = GetMethod(method);
        var restRequest = new HttpRequestMessage(httpMethod, uri);

        if (reason is not null)
        {
            restRequest.Headers.Add("X-Audit-Log-Reason", Uri.EscapeDataString(reason));
        }

        if (requestHeaders is not null)
        {
            foreach (var header in requestHeaders)
            {
                restRequest.Headers.Add(header.Key, header.Value);
            }
        }

        restRequest.Content = content;

        var response = await client.SendAsync(restRequest, cancellationTokenSource.Token);

        var headers = response
            .Headers
            .ToDictionary(key => key.Key, value => value.Value.FirstOrDefault(), StringComparer.OrdinalIgnoreCase);

        var readContent = !headerOnly || !response.IsSuccessStatusCode;
        var stream = readContent ? await response.Content.ReadAsStreamAsync(cancellationTokenSource.Token) : null;

        return new RestResponse(response.StatusCode, headers, stream);
    }

    private static MultipartFormDataContent CreateMultipartContent(IReadOnlyDictionary<string, object>? parameters)
    {
        var content = new MultipartFormDataContent($"Upload----{Guid.NewGuid():N}");

        if (parameters is null)
        {
            return content;
        }

        foreach (var (key, value) in parameters)
        {
            switch (value)
            {
                case null:
                {
                    continue;
                }

                case string stringValue:
                {
                    var stringContent = new StringContent(stringValue, Encoding.UTF8, "text/plain");

                    content.Add(stringContent, key);

                    continue;
                }
                case byte[] byteArrayValue:
                {
                    var byteArrayContent = new ByteArrayContent(byteArrayValue);

                    content.Add(byteArrayContent, key);

                    continue;
                }
                case Stream streamValue:
                {
                    var streamContent = CreateStreamContent(streamValue);

                    content.Add(streamContent, key);

                    continue;
                }
                case not null when value.GetType() == MultipartFileType:
                {
                    var stream = StreamPropertyReader(value);
                    var contentType = ContentTypePropertyReader(value);
                    var fileName = FileNamePropertyReader(value);
                    var streamContent = CreateStreamContent(stream);

                    if (contentType != null)
                    {
                        streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    }

                    content.Add(streamContent, key, fileName);

                    continue;
                }
                default:
                    throw new InvalidOperationException($"Unsupported param type \"{value.GetType().Name}\".");
            }
        }

        return content;
    }

    private static StreamContent CreateStreamContent(Stream stream)
    {
        if (stream.CanSeek)
        {
            stream.Position = 0;
        }

        return new StreamContent(stream);
    }

    private static HttpMethod GetMethod(string method)
    {
        return method switch
        {
            "DELETE" => HttpMethod.Delete,
            "GET" => HttpMethod.Get,
            "PATCH" => HttpMethod.Patch,
            "POST" => HttpMethod.Post,
            "PUT" => HttpMethod.Put,
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, $"Unknown HttpMethod: {method}")
        };
    }
}