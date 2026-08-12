using Vioren.CodebaseAtom.WebUI.Logics.Documents.UpdateDocument;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components.Documents;

public partial class DialogEditDocument
{
    [Inject]
    public required UpdateDocumentLogic UpdateDocumentLogic { get; init; }

    [Parameter]
    public required EditDocumentModel Model { get; set; }

    private readonly EditDocumentModelValidator _validator = new();
    private MudForm _form = default!;

    private async Task HandleSubmit()
    {
        try
        {
            if (!await _form.IsValidAsync())
            {
                return;
            }

            IsLoadingBase = true;
            ExceptionBase = null;

            var input = new UpdateDocumentInput
            {
                DocumentId = Model.DocumentId,
                Title = Model.Title,
                FileNameWithoutExtension = Model.FileNameWithoutExtension
            };

            await UpdateDocumentLogic.Handle(input);

            Snackbar.AddSuccess($"The {DomainDisplayTextFor.Document} has been updated successfully.");

            Dialog.Close();
        }
        catch (Exception exception)
        {
            ExceptionBase = exception;
        }
        finally
        {
            IsLoadingBase = false;
        }
    }
}

public sealed record EditDocumentModel
{
    public Guid DocumentId { get; init; }
    public required string Title { get; set; }
    public required string FileNameWithoutExtension { get; set; }
    public required string FileExtension { get; init; }
}

public sealed class EditDocumentModelValidator : AbstractValidatorBase<EditDocumentModel>
{
    public EditDocumentModelValidator()
    {
        _ = RuleFor(x => x.Title)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Document, DomainDisplayTextFor.Title))
            .MaximumLength(MaximumLengthFor.Title)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.Document, DomainDisplayTextFor.Title, MaximumLengthFor.Title));

        _ = RuleFor(x => x.FileNameWithoutExtension)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Document, DomainDisplayTextFor.FileName))
            .Must((model, fileName) => $"{fileName}{model.FileExtension}".Length <= MaximumLengthFor.FileName)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.Document, DomainDisplayTextFor.FileName, MaximumLengthFor.FileName));
    }
}
