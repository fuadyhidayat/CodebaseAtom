using System.Globalization;

namespace Vioren.CodebaseExpress.Logics.Common.Extensions;

public static class DateTimeExtensions
{
    public const string HasNoValue = "-";

    public static string ToDisplayText(this DateTime dateTime, string format)
    {
        return dateTime.ToString(format, CultureInfo.InvariantCulture);
    }

    public static string ToDisplayText(this DateTime? dateTime, string format)
    {
        if (!dateTime.HasValue)
        {
            return HasNoValue;
        }

        return dateTime.Value.ToString(format, CultureInfo.InvariantCulture);
    }

    public static DateOnly ToDateOnly(this DateTime dateTime)
    {
        return DateOnly.FromDateTime(dateTime);
    }

    public static DateOnly ToDateOnly(this DateTime? dateTime)
    {
        return dateTime.HasValue ? DateOnly.FromDateTime(dateTime.Value) : default;
    }
}
