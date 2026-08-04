namespace CodebaseAtom.WebUI.Components.Features.Home.Pages;

public partial class MySession
{
    protected override async Task OnInitializedAsync()
    {
        LoadBreadcrumbs();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(HomeBreadcrumbFor.Index);
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(UIDisplayTextFor.MySession));
    }
}
