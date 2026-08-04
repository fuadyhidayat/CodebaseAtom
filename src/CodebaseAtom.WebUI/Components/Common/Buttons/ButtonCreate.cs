namespace CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonCreate : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonCreate()
    {
        Color = Color.Dark;
        StartIcon = Icons.Material.Filled.AutoFixHigh;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Create : $"{UIDisplayTextFor.Create} {EntityType}");
    }
}
