using Vioren.CodebaseAtom.WebUI.Infrastructure.FileStorage;

namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.DownloadDocument;

public sealed class DownloadDocumentLogic(
    IDbContextFactory<DatabaseContext> databaseContextFactory,
    FileStorageService fileStorageService)
{
    public async Task<DownloadDocumentOutput> Handle(DownloadDocumentInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var document = await databaseContext.Documents
            .Where(d => d.Id == input.DocumentId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Document, DomainDisplayTextFor.Id, input.DocumentId);

        var fileContent = await fileStorageService.ReadAsync(document.FilePath, cancellationToken);

        return new DownloadDocumentOutput
        {
            Document = new DocumentDto
            {
                FileName = document.FileName,
                FileContentType = document.FileContentType,
                FileContent = fileContent
            }
        };
    }
}
