using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

public class RevalidatingIdentityAuthenticationStateProvider(
    ILoggerFactory loggerFactory,
    IServiceScopeFactory serviceScopeFactory,
    IOptions<IdentityOptions> identityOptionsAccessor)
    : RevalidatingServerAuthenticationStateProvider(loggerFactory)
{
    private readonly IdentityOptions _options = identityOptionsAccessor.Value;

    protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

    protected override async Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
    {
        var claimsPrincipal = authenticationState.User;
        var userIdClaim = claimsPrincipal.FindFirst(_options.ClaimsIdentity.UserIdClaimType);

        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return false;
        }

        using var serviceScope = serviceScopeFactory.CreateScope();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var applicationUser = await userManager.FindByIdAsync(userId.ToString());

        if (applicationUser is null)
        {
            return false;
        }

        return await ValidateSecurityStampAsync(userManager, claimsPrincipal, applicationUser);
    }

    private async Task<bool> ValidateSecurityStampAsync(UserManager<ApplicationUser> userManager, ClaimsPrincipal principal, ApplicationUser applicationUser)
    {
        var principalStamp = principal.FindFirstValue(_options.ClaimsIdentity.SecurityStampClaimType);
        var userStamp = await userManager.GetSecurityStampAsync(applicationUser);

        return principalStamp == userStamp;
    }
}
