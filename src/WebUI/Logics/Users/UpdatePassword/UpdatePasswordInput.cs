namespace Vioren.CodebaseAtom.WebUI.Logics.Users.UpdatePassword;

public sealed record UpdatePasswordInput
{
    public required Guid UserId { get; init; }
    public required string CurrentPassword { get; init; }
    public required string NewPassword { get; init; }
}
