using CodebaseAtom.WebUI.Infrastructure.FileStorage;

namespace CodebaseAtom.WebUI.Logics.Documents.DeleteDocument;

public sealed class DeleteDocumentLogic(DatabaseService databaseService, FileStorageService fileStorageService)
{
    public async Task Handle(DeleteDocumentInput input, CancellationToken cancellationToken = default)
    {
        var document = await databaseService.Documents
            .Where(document => document.Id == input.DocumentId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Document, DomainDisplayTextFor.Id, input.DocumentId);

        fileStorageService.Delete(document.FilePath);
        _ = databaseService.Documents.Remove(document);
        _ = await databaseService.SaveChangesAsync(cancellationToken);
    }
}
