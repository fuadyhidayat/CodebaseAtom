using Microsoft.AspNetCore.Identity;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity.Database.InitialData;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Identity.Database.Seeders;

public sealed class UserSeeder(
    IServiceScopeFactory serviceScopeFactory,
    IOptions<DatabaseOptions> databaseOptionsProvider)
{
    public async Task SeedUsers()
    {
        using var serviceScope = serviceScopeFactory.CreateScope();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var initialUser in InitialUsers.All)
        {
            var existingUser = await userManager.FindByIdAsync(initialUser.Id.ToString());

            if (existingUser is null)
            {
                await CreateUser(userManager, initialUser, databaseOptionsProvider.Value.DefaultPasswordForInitialUsers);
            }
        }
    }

    private static async Task CreateUser(UserManager<ApplicationUser> userManager, InitialUser initialUser, string password)
    {
        var user = new ApplicationUser
        {
            Id = initialUser.Id,
            DisplayName = initialUser.DisplayName,
            UserName = initialUser.Username,
            Email = initialUser.Email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await AssignRolesToUser(userManager, user, initialUser.Roles.Select(role => role.Name));
        }
    }

    private static async Task AssignRolesToUser(UserManager<ApplicationUser> userManager, ApplicationUser applicationUser, IEnumerable<string> roles)
    {
        foreach (var role in roles)
        {
            if (!await userManager.IsInRoleAsync(applicationUser, role))
            {
                _ = await userManager.AddToRoleAsync(applicationUser, role);
            }
        }
    }
}
