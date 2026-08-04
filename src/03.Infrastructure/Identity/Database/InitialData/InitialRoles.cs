using Vioren.CodebaseExpress.Services.Authorization.Statics;

namespace Vioren.CodebaseExpress.Infrastructure.Identity.Database.InitialData;

public static class InitialRoles
{
    public static readonly InitialRole Administrator = new()
    {
        Id = new Guid("019f187e-a0c0-7e28-be58-ae241bb9920f"),
        Name = RoleNameFor.Administrator,
    };

    public static IReadOnlyCollection<InitialRole> All =>
    [
        Administrator
    ];
}

public sealed record InitialRole
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}
