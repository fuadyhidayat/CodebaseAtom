namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonReload : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonReload()
    {
        Color = Color.Tertiary;
        StartIcon = Icons.Material.Filled.SettingsBackupRestore;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Reload : $"{UIDisplayTextFor.Reload} {EntityType}");
    }
}
