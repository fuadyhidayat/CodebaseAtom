namespace Vioren.CodebaseAtom.WebUI.Infrastructure.FileStorage;

public sealed record FileStorageOptions
{
    public const string SectionKey = nameof(FileStorage);

    public required string FolderPath { get; init; }
}
