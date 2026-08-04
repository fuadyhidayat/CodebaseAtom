namespace CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonReset : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonReset()
    {
        Color = Color.Primary;
        StartIcon = Icons.Material.Filled.Replay;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Reset : $"{UIDisplayTextFor.Reset} {EntityType}");
    }
}
