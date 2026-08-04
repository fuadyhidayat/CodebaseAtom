namespace CodebaseAtom.WebUI.Components.Common.ChipSets;

public class ChipSetDefault<T> : MudChipSet<T>
{
    public ChipSetDefault()
    {
        SelectionMode = SelectionMode.MultiSelection;
        CheckMark = true;
        Variant = Variant.Outlined;
    }
}
