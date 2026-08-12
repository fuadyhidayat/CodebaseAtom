namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.UpdateDocument;

public sealed record UpdateDocumentInput
{
    public required Guid DocumentId { get; init; }
    public required string Title { get; init; }
    public required string FileNameWithoutExtension { get; init; }
}

public sealed class UpdateDocumentInputValidator : AbstractValidatorBase<UpdateDocumentInput>
{
    public UpdateDocumentInputValidator()
    {
        _ = RuleFor(x => x.Title)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Document, DomainDisplayTextFor.Title))
            .MaximumLength(MaximumLengthFor.Title)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.Document, DomainDisplayTextFor.Title, MaximumLengthFor.Title));

        _ = RuleFor(x => x.FileNameWithoutExtension)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Document, DomainDisplayTextFor.FileName))
            .MaximumLength(MaximumLengthFor.FileName)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.Document, DomainDisplayTextFor.FileName, MaximumLengthFor.FileName));
    }
}
