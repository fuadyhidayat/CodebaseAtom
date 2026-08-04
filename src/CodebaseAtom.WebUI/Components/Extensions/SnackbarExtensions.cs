using Severity = MudBlazor.Severity;

namespace CodebaseAtom.WebUI.Components.Extensions;

public static class SnackbarExtensions
{
    public static void AddSuccess(this ISnackbar snackbar, string message)
    {
        _ = snackbar.Add(message, Severity.Success);
    }

    public static void AddInfo(this ISnackbar snackbar, string message)
    {
        _ = snackbar.Add(message, Severity.Info);
    }

    public static void AddWarning(this ISnackbar snackbar, string message)
    {
        _ = snackbar.Add(message, Severity.Warning);
    }

    public static void AddWarnings(this ISnackbar snackbar, IReadOnlyList<string> messages)
    {
        foreach (var message in messages)
        {
            _ = snackbar.Add(message, Severity.Warning);
        }
    }

    public static void AddError(this ISnackbar snackbar, string message)
    {
        _ = snackbar.Add(message, Severity.Error);
    }

    public static void AddErrors(this ISnackbar snackbar, IReadOnlyList<string> messages)
    {
        foreach (var message in messages)
        {
            _ = snackbar.Add(message, Severity.Error);
        }
    }

    public static void AddErrors(this ISnackbar snackbar, IDictionary<string, string[]> errors)
    {
        foreach (var error in errors)
        {
            foreach (var errorDetail in error.Value)
            {
                _ = snackbar.Add(errorDetail, Severity.Error);
            }
        }
    }
}
