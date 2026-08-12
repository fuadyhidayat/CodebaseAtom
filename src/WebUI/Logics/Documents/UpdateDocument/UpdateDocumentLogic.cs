namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.UpdateDocument;

public sealed class UpdateDocumentLogic(
    IDbContextFactory<DatabaseContext> databaseContextFactory,
    IValidator<UpdateDocumentInput> validator)
{
    public async Task Handle(UpdateDocumentInput input, CancellationToken cancellationToken = default)
    {
        await validator.ValidateInputAsync(input, cancellationToken);

        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var document = await databaseContext.Documents
            .Where(document => document.Id == input.DocumentId)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Document, DomainDisplayTextFor.Id, input.DocumentId);

        var fileName = $"{input.FileNameWithoutExtension}{Path.GetExtension(document.FileName)}";

        if (fileName.Length > MaximumLengthFor.FileName)
        {
            var errorMessage = ValidationMessageFor.MaximumLength(
                DomainDisplayTextFor.Document,
                DomainDisplayTextFor.FileName,
                MaximumLengthFor.FileName);

            throw new ModelValidationException(errorMessage);
        }

        document.Title = input.Title;
        document.FileName = fileName;

        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
