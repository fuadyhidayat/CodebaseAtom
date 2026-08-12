using System.Net.Mail;

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

    public static bool IsValidEmailAddress(this string text)
    {
        var trimmedEmailAddress = text.Trim();

        if (trimmedEmailAddress.EndsWith('.'))
        {
            return false;
        }

        try
        {
            var mailAddress = new MailAddress(text);
            return mailAddress.Address == trimmedEmailAddress;
        }
        catch
        {
            return false;
        }
    }
}
