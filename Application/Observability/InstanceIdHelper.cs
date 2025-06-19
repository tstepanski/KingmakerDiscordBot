using Microsoft.Extensions.Logging;

namespace KingmakerDiscordBot.Application.Observability;

internal sealed class InstanceIdHelper(HttpClient httpClient, ILogger<InstanceIdHelper> logger) : IInstanceIdHelper
{
    private const string LocalMachine = "http://169.254.169.254/latest";
    private readonly SemaphoreSlim _lock = new(1, 1);

    private string? _cachedInstanceId;

    public async Task<string> GetIdAsync(CancellationToken cancellationToken)
    {
        if (_cachedInstanceId is not null) return _cachedInstanceId;

        await _lock.WaitAsync(cancellationToken);

        try
        {
            _cachedInstanceId ??= await GetFromServerAsync(cancellationToken);

            logger.LogInformation("Resolved instance id as: '{instanceId}'", _cachedInstanceId);

            return _cachedInstanceId;
        }
        finally
        {
            _lock.Release();
        }
    }

    private async Task<string> GetFromServerAsync(CancellationToken cancellationToken)
    {
        var token = await RequestAsync(HttpMethod.Put, "api/token", "X-aws-ec2-metadata-token-ttl-seconds", "21600",
            cancellationToken);

        return await RequestAsync(HttpMethod.Get, "meta-data/instance-id", "X-aws-ec2-metadata-token", token,
            cancellationToken);
    }

    private async Task<string> RequestAsync(HttpMethod method, string path, string header, string headerValue,
        CancellationToken cancellationToken)
    {
        var metadataRequest = new HttpRequestMessage(method, $"{LocalMachine}/{path}");

        metadataRequest.Headers.Add(header, headerValue);

        var metadataResponse = await httpClient.SendAsync(metadataRequest, cancellationToken);

        metadataResponse.EnsureSuccessStatusCode();

        return await metadataResponse.Content.ReadAsStringAsync(cancellationToken);
    }
}