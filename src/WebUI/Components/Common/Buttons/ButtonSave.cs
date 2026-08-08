namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonSave : ButtonBase
{
    public ButtonSave()
    {
        Color = Color.Primary;
        StartIcon = Icons.Material.Filled.Save;
        ChildContent = builder => builder.AddContent(1, UIDisplayTextFor.Save);
    }
}
