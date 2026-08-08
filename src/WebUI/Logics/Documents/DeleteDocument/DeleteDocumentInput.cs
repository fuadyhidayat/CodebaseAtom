namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.DeleteDocument;

public sealed record DeleteDocumentInput
{
    public required Guid DocumentId { get; init; }
}
