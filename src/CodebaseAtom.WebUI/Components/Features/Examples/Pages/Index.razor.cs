namespace CodebaseAtom.WebUI.Components.Features.Examples.Pages;

public partial class Index
{
    protected override void OnInitialized()
    {
        LoadBreadcrumbs();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(HomeBreadcrumbFor.Index);
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(ExamplesDisplayTextFor.Examples));
    }
}
