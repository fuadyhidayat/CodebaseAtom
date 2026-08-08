namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonRefresh : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonRefresh()
    {
        Color = Color.Tertiary;
        StartIcon = Icons.Material.Filled.Replay;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Refresh : $"{UIDisplayTextFor.Refresh} {EntityType}");
    }
}
