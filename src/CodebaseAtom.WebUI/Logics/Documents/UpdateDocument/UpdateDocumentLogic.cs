namespace CodebaseAtom.WebUI.Logics.Documents.UpdateDocument;

public sealed class UpdateDocumentLogic(IDatabaseService databaseService)
{
    public async Task Handle(UpdateDocumentInput input, CancellationToken cancellationToken = default)
    {
        var document = await databaseService.Documents
            .Where(document => document.Id == input.DocumentId)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Document, DomainDisplayTextFor.Id, input.DocumentId);

        document.Title = input.Title;
        document.FileName = $"{input.FileNameWithoutExtension}{Path.GetExtension(document.FileName)}";

        _ = await databaseService.SaveChangesAsync(cancellationToken);
    }
}
