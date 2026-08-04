namespace CodebaseAtom.WebUI.Components.Common.Selects;

public abstract class SelectBase<T> : MudSelect<T>
{
    protected SelectBase()
    {
        Variant = Variant.Outlined;
    }
}
