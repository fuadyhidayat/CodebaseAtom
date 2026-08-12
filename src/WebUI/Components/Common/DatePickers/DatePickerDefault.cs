namespace Vioren.CodebaseAtom.WebUI.Components.Common.DatePickers;

public class DatePickerDefault : MudDatePicker
{
    public DatePickerDefault()
    {
        Required = true;
        DateFormat = "d MMMM yyyy";
        Editable = false;
        ShowToolbar = false;
        Variant = Variant.Outlined;
        MinDate = DateTime.Today;
    }
}
