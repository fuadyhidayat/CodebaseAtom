namespace CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonUpload : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonUpload()
    {
        Color = Color.Info;
        StartIcon = Icons.Material.Filled.FileUpload;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Upload : $"{UIDisplayTextFor.Upload} {EntityType}");
    }
}
