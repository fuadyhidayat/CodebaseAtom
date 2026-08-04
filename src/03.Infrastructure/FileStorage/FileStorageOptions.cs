namespace Vioren.CodebaseExpress.Infrastructure.FileStorage;

public sealed record FileStorageOptions
{
    public const string SectionKey = nameof(FileStorage);

    public required string FolderPath { get; init; }
}
