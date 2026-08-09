namespace Vioren.CodebaseAtom.WebUI.Infrastructure.FileStorage;

public sealed class FileStorageService(IOptions<FileStorageOptions> fileStorageOptionsProvider)
{
    private readonly string _folderPath = Path.IsPathRooted(fileStorageOptionsProvider.Value.FolderPath)
        ? fileStorageOptionsProvider.Value.FolderPath
        : Path.Combine(Directory.GetCurrentDirectory(), fileStorageOptionsProvider.Value.FolderPath);

    public async Task CreateAsync(string filePath, byte[] content, CancellationToken cancellationToken = default)
    {
        var subFolderPath = Path.GetDirectoryName(filePath) ?? string.Empty;
        var fileName = Path.GetFileName(filePath)
            ?? throw new ArgumentException("File name cannot be null or empty.", nameof(filePath));

        var directoryFullPath = Path.Combine(_folderPath, subFolderPath);
        _ = Directory.CreateDirectory(directoryFullPath);

        var fullFilePath = Path.Combine(directoryFullPath, fileName);
        using var fileStream = File.Create(fullFilePath);
        await fileStream.WriteAsync(content.AsMemory(0, content.Length), cancellationToken);
    }

    public void Delete(string filePath)
    {
        var subFolderPath = Path.GetDirectoryName(filePath) ?? string.Empty;
        var fileName = Path.GetFileName(filePath) ?? throw new InvalidOperationException($"File name cannot be null or empty. File path: {filePath}");

        var directoryFullPath = Path.Combine(_folderPath, subFolderPath);
        var fullFilePath = Path.Combine(directoryFullPath, fileName);

        if (File.Exists(fullFilePath))
        {
            File.Delete(fullFilePath);
        }
    }

    public Task<byte[]> ReadAsync(string filePath, CancellationToken cancellationToken = default)
    {
        var fullFilePath = Path.Combine(_folderPath, filePath);

        if (!File.Exists(fullFilePath))
        {
            throw new FileNotFoundException($"File '{filePath}' cannot be found in the folder {_folderPath}.");
        }

        return File.ReadAllBytesAsync(fullFilePath, cancellationToken);
    }
}
