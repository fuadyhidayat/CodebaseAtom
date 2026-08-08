namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonEdit : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonEdit()
    {
        Color = Color.Warning;
        StartIcon = Icons.Material.Filled.Edit;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Edit : $"{UIDisplayTextFor.Edit} {EntityType}");
    }
}
