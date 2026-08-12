using System.Collections.ObjectModel;
using Vioren.CodebaseAtom.WebUI.Logics.Documents.CreateDocument;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components.Documents;

public partial class DialogAddDocument
{
    [Inject]
    public required CreateDocumentLogic CreateDocumentLogic { get; init; }

    [Parameter]
    public required Guid ProjectId { get; set; }

    private readonly AddDocumentModel _model = new();
    private readonly AddDocumentModelValidator _validator = new();
    private MudForm _form = default!;

    private void OnFileUpdated(IBrowserFile? file)
    {
        _model.File = file;

        if (string.IsNullOrWhiteSpace(_model.Title) && file is not null)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
            _model.Title = fileNameWithoutExtension;
        }
    }

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

            if (_model.File is null)
            {
                throw new InvalidOperationException($"{DomainDisplayTextFor.Document} file is required.");
            }

            if (_model.File.Size > MaximumValueFor.DocumentFileSize)
            {
                throw new FileSizeLimitValidationException($"{DomainDisplayTextFor.Document}", _model.File.Size, MaximumValueFor.DocumentFileSize);
            }

            var fileBytes = await _model.File.ToBytesAsync(MaximumValueFor.DocumentFileSize);

            var input = new CreateDocumentInput
            {
                ProjectId = ProjectId,
                Title = _model.Title,
                FileContent = new ReadOnlyCollection<byte>(fileBytes),
                FileName = _model.File.Name,
                ContentType = _model.File.ContentType,
                FileSize = _model.File.Size
            };

            _ = await CreateDocumentLogic.Handle(input);

            Snackbar.AddSuccess($"{DomainDisplayTextFor.Document} '{_model.Title}' has been uploaded successfully.");

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

    private sealed record AddDocumentModel
    {
        public IBrowserFile? File { get; set; }
        public string Title { get; set; } = string.Empty;
    }

    private sealed class AddDocumentModelValidator : AbstractValidatorBase<AddDocumentModel>
    {
        public AddDocumentModelValidator()
        {
            _ = RuleFor(x => x.File)
                .NotNull()
                    .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Document, DomainDisplayTextFor.File));

            _ = RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Document, DomainDisplayTextFor.Title))
                .MaximumLength(MaximumLengthFor.Title)
                    .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.Document, DomainDisplayTextFor.Title, MaximumLengthFor.Title));
        }
    }
}
