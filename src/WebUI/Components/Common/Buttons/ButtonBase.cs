namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public abstract class ButtonBase : MudButton
{
    protected ButtonBase()
    {
        Variant = Variant.Filled;
        DropShadow = false;
    }
}
