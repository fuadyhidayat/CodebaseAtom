namespace Vioren.CodebaseAtom.WebUI.Components.Common.DatePickers;

public class DatePickerDefault : MudDatePicker
{
    public DatePickerDefault()
    {
        DateFormat = "d MMMM yyyy";
        Editable = false;
        ShowToolbar = false;
        Variant = Variant.Outlined;
    }
}
