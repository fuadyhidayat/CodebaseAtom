namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonReturn : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonReturn()
    {
        Color = Color.Warning;
        StartIcon = Icons.Material.Filled.KeyboardReturn;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Return : $"{UIDisplayTextFor.Return} {EntityType}");
    }
}
