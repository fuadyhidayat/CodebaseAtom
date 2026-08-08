using Vioren.CodebaseAtom.WebUI.Infrastructure.FileStorage;

namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.DeleteDocument;

public sealed class DeleteDocumentLogic(
    IDbContextFactory<DatabaseContext> databaseContextFactory,
    FileStorageService fileStorageService)
{
    public async Task Handle(DeleteDocumentInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var document = await databaseContext.Documents
            .Where(document => document.Id == input.DocumentId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Document, DomainDisplayTextFor.Id, input.DocumentId);

        fileStorageService.Delete(document.FilePath);
        _ = databaseContext.Documents.Remove(document);
        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
