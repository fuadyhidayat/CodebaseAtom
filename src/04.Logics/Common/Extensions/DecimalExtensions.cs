using System.Globalization;

namespace Vioren.CodebaseExpress.Logics.Common.Extensions;

public static class DecimalExtensions
{
    public const string HasNoValue = "-";
    private static readonly CultureInfo _englishUs = new("en-US");

    public static string ToDisplayText(this decimal value, string format)
    {
        return value.ToString(format, _englishUs);
    }

    public static string ToDisplayText(this decimal? value, string format)
    {
        if (!value.HasValue)
        {
            return HasNoValue;
        }

        return value.Value.ToString(format, _englishUs);
    }
}
