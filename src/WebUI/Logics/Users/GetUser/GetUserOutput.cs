namespace Vioren.CodebaseAtom.WebUI.Logics.Users.GetUser;

public sealed record GetUserOutput
{
    public required UserDto Item { get; init; }
}
