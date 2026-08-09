namespace Vioren.CodebaseAtom.WebUI.Logics.Users.UpdateUser;

public sealed record UpdateUserInput
{
    public required Guid UserId { get; init; }
    public required string Email { get; init; }
    public required string DisplayName { get; init; }
}
