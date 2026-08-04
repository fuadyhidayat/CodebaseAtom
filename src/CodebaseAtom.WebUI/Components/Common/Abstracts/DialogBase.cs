namespace CodebaseAtom.WebUI.Components.Common.Abstracts;

public abstract class DialogBase : AppComponentBase
{
    [CascadingParameter]
    protected IMudDialogInstance Dialog { get; init; } = default!;

    protected void Cancel()
    {
        Dialog.Cancel();
    }
}
