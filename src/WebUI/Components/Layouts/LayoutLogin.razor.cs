namespace Vioren.CodebaseAtom.WebUI.Components.Layouts;

public partial class LayoutLogin
{
    [Inject]
    public required IOptions<ApplicationOptions> ApplicationOptionsProvider { get; init; }
}
