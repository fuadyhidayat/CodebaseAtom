namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.UpdateDocument;

public sealed class UpdateDocumentLogic(DatabaseContext databaseContext)
{
    public async Task Handle(UpdateDocumentInput input, CancellationToken cancellationToken = default)
    {
        var document = await databaseContext.Documents
            .Where(document => document.Id == input.DocumentId)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Document, DomainDisplayTextFor.Id, input.DocumentId);

        document.Title = input.Title;
        document.FileName = $"{input.FileNameWithoutExtension}{Path.GetExtension(document.FileName)}";
        document.Modified = DateTimeOffset.Now;
        document.ModifiedBy = input.ModifiedBy;

        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
