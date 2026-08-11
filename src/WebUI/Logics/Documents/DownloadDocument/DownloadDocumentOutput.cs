namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.DownloadDocument;

public sealed record DownloadDocumentOutput
{
    public required DocumentDto Document { get; init; }
}
