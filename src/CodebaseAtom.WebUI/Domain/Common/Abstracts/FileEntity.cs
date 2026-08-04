namespace CodebaseAtom.WebUI.Domain.Common.Abstracts;

public abstract class FileEntity : ModifiableEntity
{
    public required string FileName { get; set; }
    public required string FileContentType { get; init; }
    public required long FileSize { get; init; }

    public string StoredFileName => $"{Id}{Path.GetExtension(FileName)}";
}
