namespace CodebaseAtom.WebUI.Components.Features.Account.Statics;

public static class RouteFor
{
    public const string AccessDenied = "Account/AccessDenied";
    public const string ForgotPassword = "Account/ForgotPassword";
    public const string Lockout = "Account/Lockout";
    public const string Logout = "Account/Logout";
    public const string ResetPassword = "Account/ResetPassword";

    public static string Login(string? returnUrl = null)
    {
        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            return "Account/Login";
        }

        return $"Account/Login?returnUrl={Uri.EscapeDataString(returnUrl)}";
    }
}
