namespace CodebaseAtom.WebUI.Logics.Documents.DownloadDocument;

public sealed record DownloadDocumentInput
{
    public required Guid DocumentId { get; init; }
}
