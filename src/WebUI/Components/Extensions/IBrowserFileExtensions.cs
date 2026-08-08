namespace Vioren.CodebaseAtom.WebUI.Components.Extensions;

public static class IBrowserFileExtensions
{
    public static async Task<byte[]> ToBytesAsync(this IBrowserFile browserFile, long maxAllowedSize, CancellationToken cancellationToken = default)
    {
        await using var sourceStream = browserFile.OpenReadStream(maxAllowedSize, cancellationToken);
        await using var memoryStream = new MemoryStream();
        await sourceStream.CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
}
