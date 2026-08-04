using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace CodebaseAtom.WebUI.Infrastructure.CurrentUser;

public sealed class CurrentUserService(AuthenticationStateProvider authenticationStateProvider)
    : ICurrentUserService
{
    public async Task<CurrentUserModel?> GetCurrentUserAsync()
    {
        AuthenticationState authenticationState;

        try
        {
            authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
        }
        catch (InvalidOperationException)
        {
            return null;
        }

        var principal = authenticationState.User;

        if (principal.Identity is null || !principal.Claims.Any())
        {
            return null;
        }

        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null)
        {
            return null;
        }

        if (!Guid.TryParse(userIdClaim.Value, out var userId))
        {
            throw new InvalidOperationException($"Invalid user ID format: {userIdClaim.Value}");
        }

        var usernameClaim = principal.FindFirst(ClaimTypes.Name);

        return new CurrentUserModel
        {
            UserId = userId,
            Username = usernameClaim is null ? "unknown" : usernameClaim.Value,
            Roles = principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList()
        };
    }
}
