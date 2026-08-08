using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Vioren.CodebaseAtom.WebUI.Logics.Documents.CreateDocument;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components.Documents;

public partial class DialogAddDocument
{
    [Inject]
    public required CreateDocumentLogic CreateDocumentLogic { get; init; }

    [Parameter]
    public required Guid ProjectId { get; set; }

    private readonly AddDocumentModel _input = new();

    private void OnFileUpdated(IBrowserFile? file)
    {
        _input.File = file;

        if (string.IsNullOrWhiteSpace(_input.Title))
        {
            if (file is not null)
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
                _input.Title = fileNameWithoutExtension;
            }
        }
    }

    private async Task OnValidSubmitAsync()
    {
        try
        {
            IsLoadingBase = true;
            ExceptionBase = null;

            if (_input.File is null)
            {
                throw new InvalidOperationException($"{DomainDisplayTextFor.Document} file is required.");
            }

            if (_input.File.Size > MaximumValueFor.DocumentFileSize)
            {
                throw new FileSizeLimitValidationException($"{DomainDisplayTextFor.Document}", _input.File.Size, MaximumValueFor.DocumentFileSize);
            }

            var fileBytes = await _input.File.ToBytesAsync(MaximumValueFor.DocumentFileSize);

            var input = new CreateDocumentInput
            {
                ProjectId = ProjectId,
                Title = _input.Title,
                FileContent = new ReadOnlyCollection<byte>(fileBytes),
                FileName = _input.File.Name,
                ContentType = _input.File.ContentType,
                FileSize = _input.File.Size
            };

            _ = await CreateDocumentLogic.Handle(input);

            Snackbar.AddSuccess($"Document '{_input.Title}' has been uploaded successfully.");

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
        [Required(ErrorMessage = "File is required.")]
        public IBrowserFile? File { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(MaximumLengthFor.Title, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;
    }
}
