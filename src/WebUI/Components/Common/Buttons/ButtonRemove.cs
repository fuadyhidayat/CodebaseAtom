namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonRemove : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonRemove()
    {
        Color = Color.Error;
        StartIcon = Icons.Material.Filled.Delete;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Remove : $"{UIDisplayTextFor.Remove} {EntityType}");
    }
}
