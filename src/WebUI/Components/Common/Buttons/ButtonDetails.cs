namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonDetails : ButtonBase
{
    public ButtonDetails()
    {
        Color = Color.Info;
        StartIcon = Icons.Material.Filled.FilterCenterFocus;
        ChildContent = builder => builder.AddContent(1, UIDisplayTextFor.Details);
    }
}
