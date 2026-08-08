namespace Vioren.CodebaseAtom.WebUI.Common.Models;

public sealed record CurrentUser
{
    public required Guid UserId { get; init; }
    public required string Username { get; init; }
    public required IReadOnlyCollection<string> Roles { get; init; }
}
