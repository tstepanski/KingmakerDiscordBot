using System.Security.Cryptography;

namespace KingmakerDiscordBot.CDK;

internal sealed class Hasher : IDisposable
{
    private readonly SHA256 _hasher = SHA256.Create();
    private bool _finalized;
    
    public void AddFile(string path)
    {
        ThrowIfFinalized();
        
        using var fileStream = File.OpenRead(path);

        var buffer = new byte[8192];
        int bytesRead;

        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
        {
            _hasher.TransformBlock(buffer, 0, bytesRead, null, 0);
        }
    }

    public override string ToString()
    {
        ThrowIfFinalized();
        
        _finalized = true;

        _hasher.TransformFinalBlock([], 0, 0);

        var hash = _hasher.Hash ?? throw new InvalidOperationException("Hash not computed");

        return Convert.ToHexStringLower(hash);
    }

    public void Dispose()
    {
        _hasher.Dispose();
    }
    
    public void Reset()
    {
        _hasher.Initialize();
        
        _finalized = false;
    }

    private void ThrowIfFinalized()
    {
        if (_finalized)
        {
            throw new InvalidOperationException("Hash already finalized");
        }
    }
}