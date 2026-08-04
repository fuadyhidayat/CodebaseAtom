namespace Vioren.CodebaseExpress.Logics.Documents.UpdateDocument;

public sealed class UpdateDocumentLogic(IDatabaseService databaseService)
    : ILogic<UpdateDocumentInput, Unit>
{
    public async Task<Unit> Handle(UpdateDocumentInput input, CancellationToken cancellationToken = default)
    {
        var document = await databaseService.Documents
            .FirstOrDefaultAsync(document => !document.IsDeleted && document.Id == input.DocumentId, cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Document, DomainDisplayTextFor.Id, input.DocumentId);

        document.Title = input.Title;
        document.FileName = $"{input.FileNameWithoutExtension}{Path.GetExtension(document.FileName)}";

        _ = await databaseService.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
