using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.CurrentUser;

public sealed class CurrentUserService(AuthenticationStateProvider authenticationStateProvider)
{
    public async Task<CurrentUserModel?> GetCurrentUserAsync()
    {
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

        var currentUser = new CurrentUserModel
        {
            UserId = userId,
            Username = claimsPrincipal.Identity?.Name ?? "unknown",
            DisplayName = claimsPrincipal.FindFirst(ClaimTypeFor.DisplayName)?.Value ?? "unknown",
            Roles = claimsPrincipal.FindAll(ClaimTypes.Role).Select(claim => claim.Value).ToList()
        };

        return currentUser;
    }
}
