namespace Vioren.CodebaseAtom.WebUI.Logics.Common.Extensions;

public static class StringExtensions
{
    public const string HasNoValue = "-";

    public static string ToDisplayText(this string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return HasNoValue;
        }

        return text;
    }
}
