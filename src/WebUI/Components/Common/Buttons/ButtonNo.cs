namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonNo : ButtonBase
{
    public ButtonNo()
    {
        Color = Color.Error;
        StartIcon = Icons.Material.Filled.ThumbDownAlt;
        ChildContent = builder => builder.AddContent(1, UIDisplayTextFor.No);
    }
}
