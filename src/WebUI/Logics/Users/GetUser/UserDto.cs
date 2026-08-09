namespace Vioren.CodebaseAtom.WebUI.Logics.Users.GetUser;

public sealed record UserDto
{
    public required Guid Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string DisplayName { get; init; }
    public required IReadOnlyCollection<string> Roles { get; init; }
}
