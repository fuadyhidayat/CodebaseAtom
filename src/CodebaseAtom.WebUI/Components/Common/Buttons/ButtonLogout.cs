namespace CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonLogout : ButtonBase
{
    public ButtonLogout()
    {
        Color = Color.Dark;
        StartIcon = Icons.Material.Filled.Logout;
        ChildContent = builder => builder.AddContent(1, UIDisplayTextFor.Logout);
    }
}
