using System.Collections.ObjectModel;

namespace Vioren.CodebaseExpress.Logics.Documents.CreateDocument;

public sealed record CreateDocumentInput
{
    public required Guid ProjectId { get; init; }
    public required string Title { get; init; }

    public required ReadOnlyCollection<byte> FileContent { get; init; }
    public required string FileName { get; init; }
    public required string ContentType { get; init; }
    public required long FileSize { get; init; }
}
