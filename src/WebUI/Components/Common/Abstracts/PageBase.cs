namespace Vioren.CodebaseAtom.WebUI.Components.Common.Abstracts;

public abstract class PageBase : AppComponentBase
{
    private readonly List<BreadcrumbItem> _breadcrumbs = [];
    public IReadOnlyList<BreadcrumbItem> BreadcrumbItems => _breadcrumbs;

    protected abstract void LoadBreadcrumbs();

    protected void AddBreadcrumb(BreadcrumbItem breadcrumb)
    {
        _breadcrumbs.Add(breadcrumb);
    }

    protected void ClearBreadcrumbs()
    {
        _breadcrumbs.Clear();
    }
}
