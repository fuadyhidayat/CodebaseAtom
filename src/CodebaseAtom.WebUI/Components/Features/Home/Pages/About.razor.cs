namespace CodebaseAtom.WebUI.Components.Features.Home.Pages;

public partial class About
{
    [Inject]
    public required IOptions<ApplicationOptions> ApplicationOptionsProvider { get; init; }

    protected override void OnInitialized()
    {
        LoadBreadcrumbs();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(HomeBreadcrumbFor.Index);
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(UIDisplayTextFor.About));
    }
}
