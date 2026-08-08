namespace Vioren.CodebaseAtom.WebUI.Logics.Common.Extensions;

public static class DateOnlyExtensions
{
    public const string HasNoValue = "-";

    public static string ToDisplayText(this DateOnly dateOnly, string format)
    {
        return dateOnly.ToString(format, CultureInfo.InvariantCulture);
    }

    public static string ToDisplayText(this DateOnly? dateOnly, string format)
    {
        if (!dateOnly.HasValue)
        {
            return HasNoValue;
        }

        return dateOnly.Value.ToString(format, CultureInfo.InvariantCulture);
    }

    public static DateTime? ToDateTimeNullable(this DateOnly dateOnly)
    {
        return dateOnly == default ? null : dateOnly.ToDateTime(TimeOnly.MinValue);
    }
}
