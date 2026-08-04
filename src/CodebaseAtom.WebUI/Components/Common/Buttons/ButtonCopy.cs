namespace CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonCopy : ButtonBase
{
    public ButtonCopy()
    {
        Color = Color.Info;
        StartIcon = Icons.Material.Filled.ContentCopy;
        ChildContent = builder => builder.AddContent(1, UIDisplayTextFor.Copy);
    }
}
