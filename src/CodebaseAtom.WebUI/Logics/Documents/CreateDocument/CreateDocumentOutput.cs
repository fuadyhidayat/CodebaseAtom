namespace CodebaseAtom.WebUI.Logics.Documents.CreateDocument;

public sealed record CreateDocumentOutput
{
    public required Guid DocumentId { get; init; }
}
