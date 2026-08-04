namespace CodebaseAtom.WebUI.Infrastructure.FileStorage;

public sealed partial class FileStorageService(IOptions<FileStorageOptions> fileStorageOptions, ILogger<FileStorageService> logger)
{
    private readonly string _folderPath = Path.IsPathRooted(fileStorageOptions.Value.FolderPath)
        ? fileStorageOptions.Value.FolderPath
        : Path.Combine(Directory.GetCurrentDirectory(), fileStorageOptions.Value.FolderPath);

    [LoggerMessage(Level = LogLevel.Information, Message = "File {fileName} in directory {directoryFullPath} is successfully created.")]
    private static partial void LogFileCreated(ILogger logger, string fileName, string directoryFullPath);

    [LoggerMessage(Level = LogLevel.Information, Message = "File {fileName} in directory {directoryFullPath} is successfully deleted.")]
    private static partial void LogFileDeleted(ILogger logger, string fileName, string directoryFullPath);

    [LoggerMessage(Level = LogLevel.Warning, Message = "File {fileName} in directory {directoryFullPath} was not found.")]
    private static partial void LogFileNotFound(ILogger logger, string fileName, string directoryFullPath);

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

        LogFileCreated(logger, fileName, directoryFullPath);
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
            LogFileDeleted(logger, fileName, directoryFullPath);
        }
        else
        {
            LogFileNotFound(logger, fileName, directoryFullPath);
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
