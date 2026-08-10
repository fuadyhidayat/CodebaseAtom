namespace Vioren.CodebaseAtom.WebUI.Logics.Users.ResetPassword;

public sealed record ResetPasswordInput
{
    public required string Username { get; init; }
    public required string Token { get; init; }
    public required string NewPassword { get; init; }
}
