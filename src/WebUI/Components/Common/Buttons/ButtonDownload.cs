namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonDownload : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonDownload()
    {
        Color = Color.Info;
        StartIcon = Icons.Material.Filled.FileDownload;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Download : $"{UIDisplayTextFor.Download} {EntityType}");
    }
}
