namespace Vioren.CodebaseAtom.WebUI.Components.Common.Buttons;

public class ButtonDelete : ButtonBase
{
    [Parameter]
    public string? EntityType { get; set; }

    public ButtonDelete()
    {
        Color = Color.Error;
        StartIcon = Icons.Material.Filled.DeleteForever;
        ChildContent = builder => builder.AddContent(1, string.IsNullOrWhiteSpace(EntityType) ? UIDisplayTextFor.Delete : $"{UIDisplayTextFor.Delete} {EntityType}");
    }
}
