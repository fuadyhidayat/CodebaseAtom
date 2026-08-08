namespace CodebaseAtom.WebUI.Components.Common.IconButtons;

public sealed class IconButtonDelete : IconButtonBase
{
    public override string TooltipText { get; set; } = UIDisplayTextFor.Delete;
    public override string Icon { get; set; } = Icons.Material.Filled.DeleteForever;
}
