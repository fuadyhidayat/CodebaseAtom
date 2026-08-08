namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonCancel : ButtonBase
{
    public ButtonCancel()
    {
        Color = Color.Default;
        StartIcon = Icons.Material.Filled.Cancel;
        ChildContent = builder => builder.AddContent(1, UIDisplayTextFor.Cancel);
    }
}
