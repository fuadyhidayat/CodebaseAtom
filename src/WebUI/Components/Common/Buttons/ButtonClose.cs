namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonClose : ButtonBase
{
    public ButtonClose()
    {
        Color = Color.Default;
        StartIcon = Icons.Material.Filled.Close;
        ChildContent = builder => builder.AddContent(1, UIDisplayTextFor.Close);
    }
}
