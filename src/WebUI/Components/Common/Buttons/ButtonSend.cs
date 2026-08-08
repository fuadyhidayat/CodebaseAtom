namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonSend : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonSend()
    {
        Color = Color.Dark;
        StartIcon = Icons.Material.Filled.Send;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Send : $"{UIDisplayTextFor.Send} {EntityType}");
    }
}
