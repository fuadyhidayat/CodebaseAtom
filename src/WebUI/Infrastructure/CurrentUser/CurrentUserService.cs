using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity.Database;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.CurrentUser;

public sealed class CurrentUserService(
    AuthenticationStateProvider authenticationStateProvider,
    IDbContextFactory<IdentityDatabaseContext> identityDatabaseContextFactory,
    CurrentUserState currentUserState)
{
    public async Task<CurrentUserModel?> GetCurrentUserAsync()
    {
        if (currentUserState.CurrentUser is not null)
        {
            Console.WriteLine("--- CurrentUserService.GetCurrentUserAsync: Current user is already cached.");

            return currentUserState.CurrentUser;
        }

        Console.WriteLine("--- CurrentUserService.GetCurrentUserAsync: Fetching current user from database.");

        var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var userClaim = authenticationState.User;

        if (userClaim.Identity is null || !userClaim.Identity.IsAuthenticated)
        {
            return null;
        }

        var userIdClaim = userClaim.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userGuid))
        {
            return null;
        }

        using var context = await identityDatabaseContextFactory.CreateDbContextAsync();

        var applicationUser = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userGuid);

        if (applicationUser is null)
        {
            return null;
        }

        var roleIds = await context.UserRoles
            .Where(ur => ur.UserId == applicationUser.Id)
            .Select(ur => ur.RoleId)
            .ToListAsync();

        var roles = await context.Roles
            .Where(r => roleIds.Contains(r.Id))
            .Select(r => r.Name ?? "unknown")
            .ToListAsync();

        var currentUser = new CurrentUserModel
        {
            UserId = applicationUser.Id,
            Username = applicationUser.UserName ?? "unknown",
            DisplayName = applicationUser.DisplayName,
            Roles = roles
        };

        Console.WriteLine($"--- CurrentUserService.GetCurrentUserAsync: Caching current user '{currentUser.Username}' with ID '{currentUser.UserId}'.");

        currentUserState.SetUser(currentUser);

        return currentUser;
    }
}
