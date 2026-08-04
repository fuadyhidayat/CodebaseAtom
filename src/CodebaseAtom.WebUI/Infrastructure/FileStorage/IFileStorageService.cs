namespace CodebaseAtom.WebUI.Infrastructure.FileStorage;

public interface IFileStorageService
{
    public Task CreateAsync(string filePath, byte[] content, CancellationToken cancellationToken = default);
    public Task DeleteAsync(string filePath, CancellationToken cancellationToken = default);
    public Task<byte[]> ReadAsync(string filePath, CancellationToken cancellationToken = default);
}
