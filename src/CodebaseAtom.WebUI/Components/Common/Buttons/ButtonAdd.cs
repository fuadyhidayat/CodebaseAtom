namespace CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonAdd : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonAdd()
    {
        Color = Color.Dark;
        StartIcon = Icons.Material.Filled.Add;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Add : $"{UIDisplayTextFor.Add} {EntityType}");
    }
}
