namespace CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonView : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonView()
    {
        Color = Color.Info;
        StartIcon = Icons.Material.Filled.RemoveRedEye;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.View : $"{UIDisplayTextFor.View} {EntityType}");
    }
}
