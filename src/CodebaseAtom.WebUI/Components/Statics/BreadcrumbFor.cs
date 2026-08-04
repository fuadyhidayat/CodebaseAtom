namespace CodebaseAtom.WebUI.Components.Statics;

public static class BreadcrumbFor
{
    public static BreadcrumbItem Active(string text, string? icon = null)
    {
        return new(text, null, disabled: true, icon: icon);
    }
}
