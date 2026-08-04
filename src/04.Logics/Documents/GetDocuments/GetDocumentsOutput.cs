namespace Vioren.CodebaseExpress.Logics.Documents.GetDocuments;

public sealed record GetDocumentsOutput
{
    public required IReadOnlyList<DocumentDto> Items { get; init; }
}
