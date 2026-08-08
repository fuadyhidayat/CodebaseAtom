namespace Vioren.CodebaseAtom.WebUI.Components.Common.IconButtons;

public sealed class IconButtonDownload : IconButtonBase
{
    public override string TooltipText { get; set; } = UIDisplayTextFor.Download;
    public override string Icon { get; set; } = Icons.Material.Filled.FileDownload;
}
