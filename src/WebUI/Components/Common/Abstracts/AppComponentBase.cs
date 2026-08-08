namespace Vioren.CodebaseAtom.WebUI.Components.Common.Abstracts;

public abstract class AppComponentBase : ComponentBase
{
    protected bool IsLoadingBase
    {
        get;

        set
        {
            field = value;

            _ = InvokeAsync(StateHasChanged);
        }
    }

    protected Exception? ExceptionBase
    {
        get;

        set
        {
            field = value;

            _ = InvokeAsync(StateHasChanged);
        }
    }
}
