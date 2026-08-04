using Microsoft.AspNetCore.Identity;
using CodebaseAtom.WebUI.Infrastructure.Identity.Database.InitialData;

namespace CodebaseAtom.WebUI.Infrastructure.Identity.Database.Seeders;

public sealed partial class RoleSeeder(RoleManager<ApplicationRole> roleManager, ILogger<RoleSeeder> logger)
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Seeding data {entityType} {entityName}...")]
    private static partial void LogSeeding(ILogger logger, string entityType, string entityName);

    public async Task SeedRoles()
    {
        foreach (var initialRole in InitialRoles.All)
        {
            var roleExists = await roleManager.RoleExistsAsync(initialRole.Name);

            if (!roleExists)
            {
                await CreateRole(initialRole);
            }
        }
    }

    private async Task CreateRole(InitialRole initialRole)
    {
        var role = new ApplicationRole
        {
            Id = initialRole.Id,
            Name = initialRole.Name
        };

        var result = await roleManager.CreateAsync(role);

        if (result.Succeeded)
        {
            LogSeeding(logger, nameof(ApplicationRole), role.Name);
        }
    }
}
