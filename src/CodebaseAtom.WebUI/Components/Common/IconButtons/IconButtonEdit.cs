namespace CodebaseAtom.WebUI.Components.Common.IconButtons;

public sealed class IconButtonEdit : IconButtonBase
{
    public override string TooltipText { get; set; } = UIDisplayTextFor.Edit;
    public override string Icon { get; set; } = Icons.Material.Filled.Edit;
}
