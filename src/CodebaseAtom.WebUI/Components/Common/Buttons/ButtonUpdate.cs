namespace CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonUpdate : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonUpdate()
    {
        Color = Color.Warning;
        StartIcon = Icons.Material.Filled.Edit;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Update : $"{UIDisplayTextFor.Update} {EntityType}");
    }
}
