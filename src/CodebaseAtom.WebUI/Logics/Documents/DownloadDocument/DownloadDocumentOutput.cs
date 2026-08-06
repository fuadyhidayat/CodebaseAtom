namespace CodebaseAtom.WebUI.Logics.Documents.DownloadDocument;

public sealed record DownloadDocumentOutput
{
    public required DocumentDto Item { get; init; }
}
