namespace CodebaseAtom.WebUI.Components.Common.IconButtons;

public sealed class IconButtonDeactivate : IconButtonBase
{
    public override string TooltipText { get; set; } = UIDisplayTextFor.Deactivate;
    public override string Icon { get; set; } = Icons.Material.Filled.PowerOff;
}
