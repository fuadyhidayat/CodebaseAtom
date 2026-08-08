namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonSubmit : ButtonBase
{
    public ButtonSubmit()
    {
        Color = Color.Primary;
        StartIcon = Icons.Material.Filled.Save;
        ChildContent = builder => builder.AddContent(1, UIDisplayTextFor.Submit);
    }
}
