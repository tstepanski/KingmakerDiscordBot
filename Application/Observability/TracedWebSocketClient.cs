using System.Runtime.ExceptionServices;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using Discord.Net.WebSockets;

namespace KingmakerDiscordBot.Application.Observability;

internal sealed class TracedWebSocketClient(IWebSocketClient client) : IWebSocketClient
{
    public void Dispose()
    {
        client.Dispose();
    }

    public void SetHeader(string key, string value)
    {
        client.SetHeader(key, value);
    }

    public void SetCancelToken(CancellationToken cancelToken)
    {
        client.SetCancelToken(cancelToken);
    }

    public Task ConnectAsync(string host)
    {
        return PerformRootOperationAsync("WebSocket.Connect", async () =>
        {
            AWSXRayRecorder.Instance.AddAnnotation("WebSocket.Host", host);

            await client.ConnectAsync(host);
        });
    }

    public Task DisconnectAsync(int closeCode = 1000)
    {
        return PerformRootOperationAsync("WebSocket.Disconnect", async () =>
        {
            AWSXRayRecorder.Instance.AddAnnotation("WebSocket.CloseCode", closeCode);
            
            await client.DisconnectAsync(closeCode);
        });
    }

    public async Task SendAsync(byte[] data, int index, int count, bool isText)
    {
        AWSXRayRecorder.Instance.BeginSubsegment("WebSocket.SendAsync");
        AWSXRayRecorder.Instance.AddMetadata("PayloadSize", count);
        AWSXRayRecorder.Instance.AddAnnotation("IsText", isText);

        try
        {
            await client.SendAsync(data, index, count, isText);
        }
        catch (Exception exception)
        {
            AWSXRayRecorder.Instance.AddException(exception);

            ExceptionDispatchInfo.Capture(exception).Throw();
        }
        finally
        {
            AWSXRayRecorder.Instance.EndSubsegment();
        }
    }

    public event Func<byte[], int, int, Task>? BinaryMessage
    {
        add => client.BinaryMessage += value;
        remove => client.BinaryMessage -= value;
    }

    public event Func<string, Task>? TextMessage
    {
        add => client.TextMessage += value;
        remove => client.TextMessage -= value;
    }

    public event Func<Exception, Task>? Closed
    {
        add => client.Closed += value;
        remove => client.Closed -= value;
    }

    private static async Task PerformRootOperationAsync(string name, Func<Task> operation)
    {
        AWSXRayRecorder.Instance.BeginSegment(name, TraceId.NewId());

        try
        {
            await operation();
        }
        catch (Exception exception)
        {
            AWSXRayRecorder.Instance.AddException(exception);

            ExceptionDispatchInfo.Capture(exception).Throw();
        }
        finally
        {
            AWSXRayRecorder.Instance.EndSegment();
        }
    }
}