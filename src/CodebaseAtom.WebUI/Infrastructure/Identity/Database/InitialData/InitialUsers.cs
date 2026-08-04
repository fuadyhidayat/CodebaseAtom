using System.Collections.ObjectModel;

namespace CodebaseAtom.WebUI.Infrastructure.Identity.Database.InitialData;

public static class InitialUsers
{
    public static readonly InitialUser Administrator = new()
    {
        Id = new Guid("019f187e-a0c0-7c9d-817f-764366d767a7"),
        Username = "admin",
        DisplayName = "Administrator",
        Email = "administrator@vioren.net",
        Roles = [InitialRoles.Administrator]
    };

    public static readonly InitialUser Condet = new()
    {
        Id = new Guid("019f187e-a0c0-729b-b87e-10a7de41b4e3"),
        Username = "condet",
        Email = "condet@vioren.net",
        DisplayName = "Condet",
        Roles = []
    };

    public static IReadOnlyCollection<InitialUser> All =>
    [
        Administrator,
        Condet
    ];
}

public sealed record InitialUser
{
    public required Guid Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string DisplayName { get; init; }

    public required Collection<InitialRole> Roles { get; init; } = [];
}
