namespace Vioren.CodebaseAtom.WebUI.Logics.Users.GetCurrentUser;

public sealed record GetCurrentUserOutput
{
    public required UserDto Item { get; init; }
}
