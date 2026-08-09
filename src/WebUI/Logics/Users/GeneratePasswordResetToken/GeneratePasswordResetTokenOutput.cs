namespace Vioren.CodebaseAtom.WebUI.Logics.Users.GeneratePasswordResetToken;

public sealed record GeneratePasswordResetTokenOutput
{
    public required string Token { get; init; }
}
