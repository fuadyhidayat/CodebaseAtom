namespace CodebaseAtom.WebUI.Components.Common.TextFields;

public class TextFieldDefault : MudTextField<string>
{
    public TextFieldDefault()
    {
        Variant = Variant.Outlined;
        Required = true;
    }
}
