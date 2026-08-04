using System.Globalization;

namespace CodebaseAtom.WebUI.Logics.Common.Extensions;

public static class DateTimeOffsetExtensions
{
    public const string HasNoValue = "-";

    public static string ToDisplayText(this DateTimeOffset dateTimeOffset, string format)
    {
        return dateTimeOffset.LocalDateTime.ToString(format, CultureInfo.InvariantCulture);
    }

    public static string ToDisplayText(this DateTimeOffset? dateTimeOffset, string format)
    {
        if (!dateTimeOffset.HasValue)
        {
            return HasNoValue;
        }

        return dateTimeOffset.Value.LocalDateTime.ToString(format, CultureInfo.InvariantCulture);
    }
}
