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

    protected override TimeSpan RevalidationInterval => TimeSpan.FromSeconds(5);

    protected override async Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
    {
        var claimsPrincipal = authenticationState.User;
        var userIdClaim = claimsPrincipal.FindFirst(_options.ClaimsIdentity.UserIdClaimType);

        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return false;
        }

        using var scope = serviceScopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return false;
        }

        Console.WriteLine($"--- Validating user with ID: {user.DisplayName}");

        return await ValidateSecurityStampAsync(userManager, claimsPrincipal, user);
    }

    private async Task<bool> ValidateSecurityStampAsync(UserManager<ApplicationUser> userManager, ClaimsPrincipal principal, ApplicationUser user)
    {
        var principalStamp = principal.FindFirstValue(_options.ClaimsIdentity.SecurityStampClaimType);
        var userStamp = await userManager.GetSecurityStampAsync(user);

        Console.WriteLine($"\tPrincipal Stamp: {principalStamp}");
        Console.WriteLine($"\tUser Stamp: {userStamp}");
        Console.WriteLine($"\tStamps match? {principalStamp == userStamp}\n");

        return principalStamp == userStamp;
    }
}
