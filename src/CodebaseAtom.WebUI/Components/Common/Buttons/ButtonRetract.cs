namespace CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonRetract : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonRetract()
    {
        Color = Color.Warning;
        StartIcon = Icons.Material.Filled.CallMissed;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Retract : $"{UIDisplayTextFor.Retract} {EntityType}");
    }
}
