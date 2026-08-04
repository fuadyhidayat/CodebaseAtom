namespace CodebaseAtom.WebUI.Logics.Documents.DownloadDocument;

public sealed record DocumentFileDto
{
    public required string FileName { get; init; }
    public required string FileContentType { get; init; }
    public required ReadOnlyMemory<byte> FileContent { get; init; }
}
