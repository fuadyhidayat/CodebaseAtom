namespace CodebaseAtom.WebUI.Components.Features.Examples.Pages;

public partial class TextFieldsUsage
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
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(ExamplesDisplayTextFor.TextFields));
    }
}
