using CodebaseAtom.WebUI.Infrastructure.FileStorage;

namespace CodebaseAtom.WebUI.Logics.Documents.DownloadDocument;

public sealed class DownloadDocumentLogic(IDatabaseService databaseService, IFileStorageService fileStorageService)
{
    public async Task<DownloadDocumentOutput> Handle(DownloadDocumentInput input, CancellationToken cancellationToken = default)
    {
        var document = await databaseService.Documents
            .Where(d => d.Id == input.DocumentId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Document, DomainDisplayTextFor.Id, input.DocumentId);

        var fileContent = await fileStorageService.ReadAsync(document.FilePath, cancellationToken);

        return new DownloadDocumentOutput
        {
            File = new DocumentFileDto
            {
                FileName = document.FileName,
                FileContentType = document.FileContentType,
                FileContent = fileContent
            }
        };
    }
}
