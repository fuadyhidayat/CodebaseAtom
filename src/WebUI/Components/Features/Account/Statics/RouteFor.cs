namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Statics;

public static class RouteFor
{
    public const string AccessDenied = $"{RouteGroupNameFor.Account}/AccessDenied";
    public const string ForgotPassword = $"{RouteGroupNameFor.Account}/ForgotPassword";
    public const string Lockout = $"{RouteGroupNameFor.Account}/Lockout";
    public const string Logout = $"{RouteGroupNameFor.Account}{RoutePatternFor.Logout}";
    public const string ResetPassword = $"{RouteGroupNameFor.Account}/ResetPassword";

    public static string Login(string? returnUrl = null)
    {
        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            return $"{RouteGroupNameFor.Account}/Login";
        }

        return $"{RouteGroupNameFor.Account}/Login?{QueryStringFor.ReturnUrl}={Uri.EscapeDataString(returnUrl)}";
    }
}
