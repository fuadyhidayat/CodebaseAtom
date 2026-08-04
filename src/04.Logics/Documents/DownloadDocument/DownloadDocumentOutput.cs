namespace Vioren.CodebaseExpress.Logics.Documents.DownloadDocument;

public sealed record DownloadDocumentOutput
{
    public required DocumentFileDto File { get; init; }
}
