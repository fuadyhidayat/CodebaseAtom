namespace CodebaseAtom.WebUI.Components.Layouts;

public partial class LayoutMain
{
    [Inject]
    public required IOptions<ApplicationOptions> ApplicationOptionsProvider { get; init; }

    private bool _drawerOpen = true;

    private void ToggleDrawer()
    {
        _drawerOpen = !_drawerOpen;

        StateHasChanged();
    }
}
