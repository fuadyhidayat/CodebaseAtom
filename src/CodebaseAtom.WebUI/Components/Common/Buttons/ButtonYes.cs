namespace CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonYes : ButtonBase
{
    public ButtonYes()
    {
        Color = Color.Success;
        StartIcon = Icons.Material.Filled.ThumbUpAlt;
        ChildContent = builder => builder.AddContent(1, UIDisplayTextFor.Yes);
    }
}
