namespace CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonPublish : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonPublish()
    {
        Color = Color.Dark;
        StartIcon = Icons.Material.Filled.RocketLaunch;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Publish : $"{UIDisplayTextFor.Publish} {EntityType}");
    }
}
