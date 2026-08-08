namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages;

public partial class AccessDenied
{
    protected override void OnInitialized()
    {
        LoadBreadcrumbs();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(HomeBreadcrumbFor.Index);
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(UIDisplayTextFor.AccessDenied));
    }
}
