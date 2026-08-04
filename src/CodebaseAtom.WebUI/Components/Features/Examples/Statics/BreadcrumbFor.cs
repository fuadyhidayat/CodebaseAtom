namespace CodebaseAtom.WebUI.Components.Features.Examples.Statics;

public static class BreadcrumbFor
{
    public static readonly BreadcrumbItem Index = new(ExamplesDisplayTextFor.Examples, ExamplesRouteFor.Index);
    public static readonly BreadcrumbItem Loadings = ComponentsBreadcrumbFor.Active(ExamplesDisplayTextFor.Loadings);
}
