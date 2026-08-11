namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.DeleteDocuments;

public sealed record DeleteDocumentsInput
{
    public required IEnumerable<Guid> DocumentIds { get; init; }
}
