namespace Vioren.CodebaseAtom.WebUI.Components.Features.Examples.Pages.Loadings;

public partial class LoadingOverlayUsage
{
    protected override void OnInitialized()
    {
        LoadBreadcrumbs();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(HomeBreadcrumbFor.Index);
        AddBreadcrumb(ExamplesBreadcrumbFor.Index);
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(ExamplesDisplayTextFor.LoadingOverlay));
    }

    private async Task ShowLoading()
    {
        IsLoadingBase = true;

        await Task.Delay(3000);

        IsLoadingBase = false;
    }
}
