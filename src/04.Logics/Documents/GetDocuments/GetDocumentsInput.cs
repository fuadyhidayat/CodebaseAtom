namespace Vioren.CodebaseExpress.Logics.Documents.GetDocuments;

public sealed record GetDocumentsInput
{
    public required Guid ProjectId { get; init; }
}
