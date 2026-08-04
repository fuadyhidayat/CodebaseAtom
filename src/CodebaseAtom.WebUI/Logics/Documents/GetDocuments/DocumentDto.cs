namespace CodebaseAtom.WebUI.Logics.Documents.GetDocuments;

public sealed record DocumentDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string FileName { get; init; }
    public required long FileSize { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
}
