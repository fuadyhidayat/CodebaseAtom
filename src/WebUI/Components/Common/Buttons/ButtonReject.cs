namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonReject : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonReject()
    {
        Color = Color.Error;
        StartIcon = Icons.Material.Filled.ThumbDown;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Reject : $"{UIDisplayTextFor.Reject} {EntityType}");
    }
}
