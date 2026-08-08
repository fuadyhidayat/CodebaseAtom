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
            return currentUserState.CurrentUser;
        }

        var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var claimsPrincipal = authenticationState.User;

        if (claimsPrincipal.Identity is null || !claimsPrincipal.Identity.IsAuthenticated)
        {
            return null;
        }

        var claimUserId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);

        if (claimUserId is null || !Guid.TryParse(claimUserId.Value, out var userId))
        {
            return null;
        }

        using var identityDatabaseContext = await identityDatabaseContextFactory.CreateDbContextAsync();

        var applicationUser = await identityDatabaseContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == userId);

        if (applicationUser is null)
        {
            return null;
        }

        var roleIds = await identityDatabaseContext.UserRoles
            .Where(userRole => userRole.UserId == applicationUser.Id)
            .Select(userRole => userRole.RoleId)
            .ToListAsync();

        var roles = await identityDatabaseContext.Roles
            .Where(role => roleIds.Contains(role.Id))
            .Select(role => role.Name!)
            .ToListAsync();

        var currentUser = new CurrentUserModel
        {
            UserId = applicationUser.Id,
            Username = applicationUser.UserName ?? "unknown",
            DisplayName = applicationUser.DisplayName,
            Roles = roles
        };

        currentUserState.SetUser(currentUser);

        return currentUser;
    }
}
