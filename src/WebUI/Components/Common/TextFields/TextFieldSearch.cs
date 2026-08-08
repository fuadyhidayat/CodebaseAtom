namespace Vioren.CodebaseAtom.WebUI.Components.Common.TextFields;

public class TextFieldSearch : MudTextField<string>
{
    public TextFieldSearch()
    {
        Variant = Variant.Text;
        Placeholder = "Search";
        Adornment = Adornment.End;
        AdornmentIcon = Icons.Material.Filled.Search;
        Immediate = true;
        Clearable = true;
    }
}
