namespace CodebaseAtom.WebUI.Components.Common.IconButtons;

public sealed class IconButtonDetails : IconButtonBase
{
    public override string TooltipText { get; set; } = UIDisplayTextFor.Details;
    public override string Icon { get; set; } = Icons.Material.Filled.RemoveRedEye;
}
