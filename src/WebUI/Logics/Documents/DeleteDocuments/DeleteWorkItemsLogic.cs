using Vioren.CodebaseAtom.WebUI.Infrastructure.FileStorage;

namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.DeleteDocuments;

public sealed class DeleteDocumentsLogic(
    IDbContextFactory<DatabaseContext> databaseContextFactory,
    FileStorageService fileStorageService)
{
    public async Task Handle(DeleteDocumentsInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var documents = await databaseContext.Documents
            .Where(document => input.DocumentIds.Contains(document.Id))
            .ToListAsync(cancellationToken);

        foreach (var document in documents)
        {
            fileStorageService.Delete(document.FilePath);
        }

        databaseContext.Documents.RemoveRange(documents);
        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
