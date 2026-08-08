namespace CodebaseAtom.WebUI.Components.Common.IconButtons;

public sealed class IconButtonActivate : IconButtonBase
{
    public override string TooltipText { get; set; } = UIDisplayTextFor.Activate;
    public override string Icon { get; set; } = Icons.Material.Filled.Power;
}
