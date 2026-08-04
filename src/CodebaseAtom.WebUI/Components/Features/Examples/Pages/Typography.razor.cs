namespace CodebaseAtom.WebUI.Components.Features.Examples.Pages;

public partial class Typography
{
    private const string _pangram1 = "The quick brown fox jumps over the lazy dog";
    private const string _pangram2 = "The five boxing wizards jump quickly";
    private string _text = _pangram1;

    protected override void OnInitialized()
    {
        LoadBreadcrumbs();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(HomeBreadcrumbFor.Index);
        AddBreadcrumb(ExamplesBreadcrumbFor.Index);
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(ExamplesDisplayTextFor.Typography));
    }

    protected void SetToPangram1()
    {
        _text = _pangram1;
    }

    protected void SetToPangram2()
    {
        _text = _pangram2;
    }

    protected void SetToCurrentDateTime()
    {
        _text = $"What time is it? It's {DateTime.Now: dddd MMMM yyyy, HH:mm:ss}!";
    }
}
