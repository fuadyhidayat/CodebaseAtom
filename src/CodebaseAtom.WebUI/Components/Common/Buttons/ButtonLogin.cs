namespace CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonLogin : ButtonBase
{
    public ButtonLogin()
    {
        Color = Color.Primary;
        StartIcon = Icons.Material.Filled.Login;
        ChildContent = builder => builder.AddContent(1, UIDisplayTextFor.Login);
    }
}
