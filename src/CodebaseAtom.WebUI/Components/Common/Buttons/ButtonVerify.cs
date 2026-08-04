namespace CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonVerify : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonVerify()
    {
        Color = Color.Primary;
        StartIcon = Icons.Material.Filled.VerifiedUser;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Verify : $"{UIDisplayTextFor.Verify} {EntityType}");
    }
}
