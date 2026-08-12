namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonChange : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonChange()
    {
        Color = Color.Warning;
        StartIcon = Icons.Material.Filled.Edit;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Change : $"{UIDisplayTextFor.Change} {EntityType}");
    }
}
