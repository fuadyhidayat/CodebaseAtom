namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonOpen : ButtonBase
{
    public ButtonOpen()
    {
        Color = Color.Info;
        StartIcon = Icons.Material.Filled.OpenInBrowser;
        ChildContent = builder => builder.AddContent(1, UIDisplayTextFor.Open);
    }
}
