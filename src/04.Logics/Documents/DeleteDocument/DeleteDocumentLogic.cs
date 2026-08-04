using Vioren.CodebaseExpress.Services.FileStorage;

namespace Vioren.CodebaseExpress.Logics.Documents.DeleteDocument;

public sealed class DeleteDocumentLogic(
    IDatabaseService databaseService,
    IFileStorageService fileStorageService)
    : ILogic<DeleteDocumentInput, Unit>
{
    public async Task<Unit> Handle(DeleteDocumentInput input, CancellationToken cancellationToken = default)
    {
        var document = await databaseService.Documents
            .Where(document => document.Id == input.DocumentId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Document, DomainDisplayTextFor.Id, input.DocumentId);

        await fileStorageService.DeleteAsync(document.FilePath, cancellationToken);
        _ = databaseService.Documents.Remove(document);
        _ = await databaseService.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
