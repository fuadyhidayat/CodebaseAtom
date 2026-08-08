namespace Vioren.CodebaseAtom.WebUI.Components.Common.FileUploads;

public partial class FileUploadMultiple
{
    [Parameter, EditorRequired]
    public EventCallback<IReadOnlyList<IBrowserFile>> OnFilesUpdated { get; set; }

    [Parameter]
    public bool DragAndDrop { get; set; } = true;

    [Parameter, EditorRequired]
    public string Accept { get; set; }

    private IReadOnlyList<IBrowserFile> _files = [];

    private async Task OnFilesChanged(InputFileChangeEventArgs e)
    {
        await OnFilesUpdated.InvokeAsync(_files);
    }

    private async Task RemoveFile(IBrowserFile file)
    {
        _files = _files.Where(f => f != file).ToList();

        await OnFilesUpdated.InvokeAsync(_files);
    }
}
