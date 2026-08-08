namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonApply : ButtonBase
{
    public ButtonApply()
    {
        Color = Color.Primary;
        StartIcon = Icons.Material.Filled.Check;
        ChildContent = builder => builder.AddContent(1, UIDisplayTextFor.Apply);
    }
}
