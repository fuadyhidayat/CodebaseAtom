namespace CodebaseAtom.WebUI.Logics.Documents.GetDocuments;

public sealed record GetDocumentsInput
{
    public required Guid ProjectId { get; init; }
}
