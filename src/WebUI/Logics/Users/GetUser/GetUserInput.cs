namespace Vioren.CodebaseAtom.WebUI.Logics.Users.GetUser;

public sealed record GetUserInput
{
    public required Guid Id { get; init; }
}
