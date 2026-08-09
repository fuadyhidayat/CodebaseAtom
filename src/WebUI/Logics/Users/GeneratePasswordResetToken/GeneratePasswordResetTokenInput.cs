namespace Vioren.CodebaseAtom.WebUI.Logics.Users.GeneratePasswordResetToken;

public sealed record GeneratePasswordResetTokenInput
{
    public required string Email { get; init; }
}
