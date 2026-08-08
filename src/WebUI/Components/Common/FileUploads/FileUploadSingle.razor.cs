namespace Vioren.CodebaseAtom.WebUI.Components.Common.FileUploads;

public partial class FileUploadSingle
{
    [Parameter, EditorRequired]
    public EventCallback<IBrowserFile?> OnFileUpdated { get; set; }

    [Parameter]
    public bool DragAndDrop { get; set; } = true;

    [Parameter, EditorRequired]
    public string Accept { get; set; }

    private IBrowserFile? _file;
    private bool _disabled;

    private async Task OnFilesChanged(InputFileChangeEventArgs e)
    {
        _disabled = e.FileCount > 0;
        await OnFileUpdated.InvokeAsync(_file);
    }

    private async Task RemoveFile()
    {
        _disabled = false;
        _file = null;
        await OnFileUpdated.InvokeAsync(_file);
    }
}
