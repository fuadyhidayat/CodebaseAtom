namespace Vioren.CodebaseAtom.WebUI.Logics.Users.ResetPassword;

public sealed record ResetPasswordInput
{
    public required string Username { get; init; }
    public required string Code { get; init; }
    public required string Password { get; init; }
}
