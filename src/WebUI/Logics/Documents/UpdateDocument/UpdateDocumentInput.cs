namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.UpdateDocument;

public sealed record UpdateDocumentInput
{
    public required Guid DocumentId { get; init; }
    public required Guid ModifiedBy { get; init; }
    public required string Title { get; init; }
    public required string FileNameWithoutExtension { get; init; }
}
