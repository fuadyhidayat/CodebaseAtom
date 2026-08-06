using CodebaseAtom.WebUI.Logics.Documents.DeleteDocument;
using CodebaseAtom.WebUI.Logics.Documents.DownloadDocument;
using CodebaseAtom.WebUI.Logics.Documents.GetDocuments;
using Microsoft.JSInterop;

namespace CodebaseAtom.WebUI.Components.Features.Projects.Components.Documents;

public partial class TabPanelDocuments
{
    [Inject]
    public required IDialogService DialogService { get; init; }

    [Inject]
    public required GetDocumentsLogic GetDocumentsLogic { get; set; }

    [Inject]
    public required DeleteDocumentLogic DeleteDocumentLogic { get; set; }

    [Inject]
    public required DownloadDocumentLogic DownloadDocumentLogic { get; set; }

    [Inject]
    public required IJSRuntime JsRuntime { get; set; }

    [Parameter, EditorRequired]
    public Guid ProjectId { get; set; }

    private string _searchKeyword = string.Empty;
    private List<DocumentModel> _items = new();

    protected override async Task OnParametersSetAsync()
    {
        await LoadItems();
    }

    private async Task LoadItems()
    {
        try
        {
            IsLoadingBase = true;

            var output = await GetDocumentsLogic.Handle(new GetDocumentsInput
            {
                ProjectId = ProjectId
            });

            _items = output.Items.Select(item => new DocumentModel
            {
                Id = item.Id,
                Title = item.Title,
                FileName = item.FileName,
                FileSize = item.FileSize,
                CreatedAt = item.CreatedAt
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

    private bool FilterItems(DocumentModel item)
    {
        if (string.IsNullOrWhiteSpace(_searchKeyword))
        {
            return true;
        }

        if (item.Title.Contains(_searchKeyword))
        {
            return true;
        }

        if (item.FileName.Contains(_searchKeyword))
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
            await LoadItems();
        }
    }

    private async Task HandleDownloadDocument(DocumentModel item)
    {
        try
        {
            IsLoadingBase = true;

            var output = await DownloadDocumentLogic.Handle(new DownloadDocumentInput
            {
                DocumentId = item.Id
            });

            await JsRuntime.InvokeVoidAsync(
                "downloadFileFromStream",
                output.Item.FileName,
                output.Item.FileContentType,
                output.Item.FileContent.ToArray()
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

    private async Task ShowDialogEditDocument(DocumentModel item)
    {
        var model = new EditDocumentModel
        {
            DocumentId = item.Id,
            Title = item.Title,
            FileNameWithoutExtension = Path.GetFileNameWithoutExtension(item.FileName),
            FileExtension = Path.GetExtension(item.FileName)
        };

        var parameters = new DialogParameters
        {
            { nameof(DialogEditDocument.Model), model }
        };

        var dialog = await DialogService.ShowAsync<DialogEditDocument>($"{UIDisplayTextFor.Edit} {DomainDisplayTextFor.Document}", parameters);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            await LoadItems();
        }
    }

    private async Task ShowDialogDeleteDocument(DocumentModel item)
    {
        var dialogResult = await DialogService.ShowMessageBoxAsync(
          $"{UIDisplayTextFor.Delete} {DomainDisplayTextFor.Document}",
          ConfirmationMessageFor.Delete(DomainDisplayTextFor.Document, item.Title),
          yesText: UIDisplayTextFor.Yes,
          noText: UIDisplayTextFor.No,
          options: new DialogOptions { MaxWidth = MaxWidth.ExtraSmall });

        if (dialogResult is true)
        {
            await DeleteDocument(item.Id);
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
            await LoadItems();
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
