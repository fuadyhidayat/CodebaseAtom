namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.UpdateDocument;

public sealed class UpdateDocumentLogic(IDbContextFactory<DatabaseContext> databaseContextFactory)
{
    public async Task Handle(UpdateDocumentInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var document = await databaseContext.Documents
            .Where(document => document.Id == input.DocumentId)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Document, DomainDisplayTextFor.Id, input.DocumentId);

        document.Title = input.Title;
        document.FileName = $"{input.FileNameWithoutExtension}{Path.GetExtension(document.FileName)}";

        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
