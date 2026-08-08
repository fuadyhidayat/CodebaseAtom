namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonConfirm : ButtonBase
{
    public ButtonConfirm()
    {
        Color = Color.Primary;
        StartIcon = Icons.Material.Filled.Check;
        ChildContent = builder => builder.AddContent(1, UIDisplayTextFor.Confirm);
    }
}
