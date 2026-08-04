using Microsoft.AspNetCore.Identity;
using CodebaseAtom.WebUI.Infrastructure.Identity.Database.InitialData;

namespace CodebaseAtom.WebUI.Infrastructure.Identity.Database.Seeders;

public sealed partial class UserSeeder(
    UserManager<ApplicationUser> userManager,
    IOptions<IdentityOptions> identityOptionsProvider,
    ILogger<UserSeeder> logger)
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Seeding data {entityType} {entityName}...")]
    private static partial void LogSeeding(ILogger logger, string entityType, string entityName);

    public async Task SeedUsers()
    {
        foreach (var initialUser in InitialUsers.All)
        {
            var existingUser = await userManager.FindByIdAsync(initialUser.Id.ToString());

            if (existingUser is null)
            {
                await CreateUser(initialUser);
            }
        }
    }

    private async Task CreateUser(InitialUser initialUser)
    {
        var user = new ApplicationUser
        {
            Id = initialUser.Id,
            DisplayName = initialUser.DisplayName,
            UserName = initialUser.Username,
            Email = initialUser.Email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, identityOptionsProvider.Value.DefaultPasswordForInitialUsers);

        if (result.Succeeded)
        {
            LogSeeding(logger, nameof(ApplicationUser), user.UserName);

            await AssignRolesToUser(user, initialUser.Roles.Select(role => role.Name));
        }
    }

    private async Task AssignRolesToUser(ApplicationUser applicationUser, IEnumerable<string> roles)
    {
        foreach (var role in roles)
        {
            if (!await userManager.IsInRoleAsync(applicationUser, role))
            {
                var result = await userManager.AddToRoleAsync(applicationUser, role);

                if (result.Succeeded)
                {
                    var info = $"{applicationUser.UserName} - {role}";

                    LogSeeding(logger, nameof(IdentityUserRole<>), info);
                }
            }
        }
    }
}
