using System.Collections.ObjectModel;

namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.CreateDocument;

public sealed record CreateDocumentInput
{
    public required Guid ProjectId { get; init; }
    public required string Title { get; init; }
    public required ReadOnlyCollection<byte> FileContent { get; init; }
    public required string FileName { get; init; }
    public required string ContentType { get; init; }
    public required long FileSize { get; init; }
}

public sealed class CreateDocumentInputValidator : AbstractValidatorBase<CreateDocumentInput>
{
    public CreateDocumentInputValidator()
    {
        _ = RuleFor(x => x.Title)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Document, DomainDisplayTextFor.Title))
            .MaximumLength(MaximumLengthFor.Title)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.Document, DomainDisplayTextFor.Title, MaximumLengthFor.Title));

        _ = RuleFor(x => x.FileContent)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Document, DomainDisplayTextFor.File))
            .Must(fileContent => fileContent is null || fileContent.Count <= MaximumValueFor.DocumentFileSize)
                .WithMessage($"The maximum {DomainDisplayTextFor.Document} {DomainDisplayTextFor.FileSize} is {MaximumValueFor.DocumentFileSize} bytes.")
            .Must((input, fileContent) => fileContent is null || fileContent.Count == input.FileSize)
                .WithMessage($"{DomainDisplayTextFor.Document} {DomainDisplayTextFor.FileSize} does not match its content length.");

        _ = RuleFor(x => x.FileName)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Document, DomainDisplayTextFor.FileName))
            .MaximumLength(MaximumLengthFor.FileName)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.Document, DomainDisplayTextFor.FileName, MaximumLengthFor.FileName));

        _ = RuleFor(x => x.ContentType)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Document, DomainDisplayTextFor.ContentType))
            .MaximumLength(MaximumLengthFor.FileContentType)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.Document, DomainDisplayTextFor.ContentType, MaximumLengthFor.FileContentType));

        _ = RuleFor(x => x.FileSize)
            .GreaterThan(0)
                .WithMessage($"{DomainDisplayTextFor.Document} {DomainDisplayTextFor.FileSize} must be greater than zero.")
            .LessThanOrEqualTo(MaximumValueFor.DocumentFileSize)
                .WithMessage($"The maximum {DomainDisplayTextFor.Document} {DomainDisplayTextFor.FileSize} is {MaximumValueFor.DocumentFileSize} bytes.");
    }
}
