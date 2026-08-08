namespace Vioren.CodebaseAtom.WebUI.Infrastructure.CurrentUser;

public sealed record CurrentUserModel
{
    public required Guid UserId { get; init; }
    public required string Username { get; init; }
    public required string DisplayName { get; init; }
    public required IReadOnlyCollection<string> Roles { get; init; }
}
