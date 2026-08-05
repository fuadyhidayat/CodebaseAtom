using Microsoft.AspNetCore.Identity;
using CodebaseAtom.WebUI.Infrastructure.Identity.Database.InitialData;

namespace CodebaseAtom.WebUI.Infrastructure.Identity.Database.Seeders;

public sealed class RoleSeeder(RoleManager<ApplicationRole> roleManager)
{
    public async Task SeedRoles()
    {
        foreach (var initialRole in InitialRoles.All)
        {
            var roleExists = await roleManager.RoleExistsAsync(initialRole.Name);

            if (!roleExists)
            {
                var role = new ApplicationRole
                {
                    Id = initialRole.Id,
                    Name = initialRole.Name
                };

                _ = await roleManager.CreateAsync(role);
            }
        }
    }
}
