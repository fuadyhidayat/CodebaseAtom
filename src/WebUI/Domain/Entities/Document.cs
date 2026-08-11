namespace Vioren.CodebaseAtom.WebUI.Domain.Entities;

public sealed class Document : FileEntity
{
    public required Guid ProjectId { get; init; }
    public Project Project { get; init; } = default!;

    public required string Title { get; set; }

    public const string RootFolderName = "Documents";
    public string FolderPath => Path.Combine(RootFolderName, ProjectId.ToString(), CreatedAt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
    public string FilePath => Path.Combine(FolderPath, StoredFileName);
}
