using Microsoft.JSInterop;
using Vioren.CodebaseAtom.WebUI.Logics.Documents.DeleteDocument;
using Vioren.CodebaseAtom.WebUI.Logics.Documents.DeleteDocuments;
using Vioren.CodebaseAtom.WebUI.Logics.Documents.DownloadDocument;
using Vioren.CodebaseAtom.WebUI.Logics.Documents.GetDocuments;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components.Documents;

public partial class TabContentDocuments
{
    [Inject]
    public required IDialogService DialogService { get; init; }

    [Inject]
    public required GetDocumentsLogic GetDocumentsLogic { get; set; }

    [Inject]
    public required DeleteDocumentLogic DeleteDocumentLogic { get; set; }

    [Inject]
    public required DeleteDocumentsLogic DeleteDocumentsLogic { get; set; }

    [Inject]
    public required DownloadDocumentLogic DownloadDocumentLogic { get; set; }

    [Inject]
    public required IJSRuntime JsRuntime { get; set; }

    [Parameter, EditorRequired]
    public Guid ProjectId { get; set; }

    private string _searchKeyword = string.Empty;
    private List<DocumentModel> _documents = default!;
    private HashSet<DocumentModel> _selectedDocuments = new();

    protected override async Task OnParametersSetAsync()
    {
        await LoadDocuments();
    }

    private async Task LoadDocuments()
    {
        try
        {
            IsLoadingBase = true;

            var output = await GetDocumentsLogic.Handle(new GetDocumentsInput
            {
                ProjectId = ProjectId
            });

            _documents = output.Documents.Select(document => new DocumentModel
            {
                Id = document.Id,
                Title = document.Title,
                FileName = document.FileName,
                FileSize = document.FileSize,
                CreatedAt = document.CreatedAt
            }).ToList();
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

    private bool FilterDocuments(DocumentModel document)
    {
        if (string.IsNullOrWhiteSpace(_searchKeyword))
        {
            return true;
        }

        if (document.Title.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (document.FileName.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }

    private async Task ShowDialogAddDocument()
    {
        var parameters = new DialogParameters
        {
            { nameof(DialogAddDocument.ProjectId), ProjectId }
        };

        var dialog = await DialogService.ShowAsync<DialogAddDocument>($"{UIDisplayTextFor.Add} {DomainDisplayTextFor.Document}", parameters);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            await LoadDocuments();
        }
    }

    private async Task HandleDownloadDocument(DocumentModel document)
    {
        try
        {
            IsLoadingBase = true;

            var output = await DownloadDocumentLogic.Handle(new DownloadDocumentInput
            {
                DocumentId = document.Id
            });

            await JsRuntime.InvokeVoidAsync(
                "downloadFileFromStream",
                output.Document.FileName,
                output.Document.FileContentType,
                output.Document.FileContent.ToArray()
            );
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

    private async Task ShowDialogEditDocument(DocumentModel document)
    {
        var model = new EditDocumentModel
        {
            DocumentId = document.Id,
            Title = document.Title,
            FileNameWithoutExtension = Path.GetFileNameWithoutExtension(document.FileName),
            FileExtension = Path.GetExtension(document.FileName)
        };

        var parameters = new DialogParameters
        {
            { nameof(DialogEditDocument.Model), model }
        };

        var dialog = await DialogService.ShowAsync<DialogEditDocument>($"{UIDisplayTextFor.Edit} {DomainDisplayTextFor.Document}", parameters);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            await LoadDocuments();
        }
    }

    private async Task ShowDialogDeleteDocument(DocumentModel document)
    {
        var dialogResult = await DialogService.ShowMessageBoxAsync(
          $"{UIDisplayTextFor.Delete} {DomainDisplayTextFor.Document}",
          ConfirmationMessageFor.Delete(DomainDisplayTextFor.Document, document.Title),
          yesText: UIDisplayTextFor.Yes,
          noText: UIDisplayTextFor.No,
          options: new DialogOptions { MaxWidth = MaxWidth.ExtraSmall });

        if (dialogResult is true)
        {
            await DeleteDocument(document.Id);
        }
    }

    private async Task DeleteDocument(Guid documentId)
    {
        try
        {
            IsLoadingBase = true;
            ExceptionBase = null;

            var input = new DeleteDocumentInput
            {
                DocumentId = documentId
            };

            await DeleteDocumentLogic.Handle(input);
            await LoadDocuments();
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

    private async Task ShowDialogDeleteSelectedDocuments()
    {
        if (_selectedDocuments.Count == 0)
        {
            return;
        }

        var entityDisplayText = _selectedDocuments.Count is 1 ? DomainDisplayTextFor.Document : DomainDisplayTextFor.Documents;
        var dialogResult = await DialogService.ShowMessageBoxAsync(
          $"{UIDisplayTextFor.Delete} {DomainDisplayTextFor.Documents}",
          $"Are you sure you want to delete the selected {_selectedDocuments.Count} {entityDisplayText}?",
          yesText: UIDisplayTextFor.Yes,
          noText: UIDisplayTextFor.No,
          options: new DialogOptions { MaxWidth = MaxWidth.ExtraSmall });

        if (dialogResult is true)
        {
            await DeleteDocuments(_selectedDocuments.Select(x => x.Id));
        }
    }

    private async Task DeleteDocuments(IEnumerable<Guid> documentIds)
    {
        try
        {
            IsLoadingBase = true;

            var input = new DeleteDocumentsInput
            {
                DocumentIds = documentIds
            };

            await DeleteDocumentsLogic.Handle(input);
            Snackbar.AddSuccess($"{documentIds.Count()} {DomainDisplayTextFor.Documents} deleted successfully.");
            await LoadDocuments();
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

    private sealed record DocumentModel
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required string FileName { get; init; }
        public required long FileSize { get; init; }
        public required DateTimeOffset CreatedAt { get; init; }
    }
}
