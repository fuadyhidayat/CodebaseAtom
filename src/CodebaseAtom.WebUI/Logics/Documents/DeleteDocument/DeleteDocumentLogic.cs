using CodebaseAtom.WebUI.Infrastructure.FileStorage;

namespace CodebaseAtom.WebUI.Logics.Documents.DeleteDocument;

public sealed class DeleteDocumentLogic(DatabaseContext databaseContext, FileStorageService fileStorageService)
{
    public async Task Handle(DeleteDocumentInput input, CancellationToken cancellationToken = default)
    {
        var document = await databaseContext.Documents
            .Where(document => document.Id == input.DocumentId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Document, DomainDisplayTextFor.Id, input.DocumentId);

        fileStorageService.Delete(document.FilePath);
        _ = databaseContext.Documents.Remove(document);
        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
