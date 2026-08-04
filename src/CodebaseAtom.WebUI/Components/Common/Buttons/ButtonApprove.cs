namespace CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonApprove : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonApprove()
    {
        Color = Color.Success;
        StartIcon = Icons.Material.Filled.ThumbUp;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Approve : $"{UIDisplayTextFor.Approve} {EntityType}");
    }
}
