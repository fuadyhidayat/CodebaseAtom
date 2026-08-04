using CodebaseAtom.WebUI.Components.Statics;

namespace CodebaseAtom.WebUI.Components.Layouts;

public partial class LayoutLogin
{
    [Inject]
    public required IOptions<ApplicationOptions> ApplicationOptionsProvider { get; init; }

    private readonly MudTheme _theme = ThemeFor.Default.Clone();

    protected override void OnInitialized()
    {
        _theme.LayoutProperties.AppbarHeight = "0px";
    }
}
