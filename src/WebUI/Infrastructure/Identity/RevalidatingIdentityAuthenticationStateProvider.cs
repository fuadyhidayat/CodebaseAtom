using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using AspNetIdentityOptions = Microsoft.AspNetCore.Identity.IdentityOptions;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

public class RevalidatingIdentityAuthenticationStateProvider<TUser>
    : RevalidatingServerAuthenticationStateProvider where TUser : class
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly AspNetIdentityOptions _options;

    public RevalidatingIdentityAuthenticationStateProvider(
        ILoggerFactory loggerFactory,
        IServiceScopeFactory scopeFactory,
        IOptions<AspNetIdentityOptions> optionsAccessor)
        : base(loggerFactory)
    {
        _scopeFactory = scopeFactory;
        _options = optionsAccessor.Value;
    }

    protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(1);

    protected override async Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
    {
        Console.WriteLine("--- RevalidatingIdentityAuthenticationStateProvider: ValidateAuthenticationStateAsync called.");

        var user = authenticationState.User;
        var userIdClaim = user.FindFirst(_options.ClaimsIdentity.UserIdClaimType);

        Console.WriteLine($"--- RevalidatingIdentityAuthenticationStateProvider: User ID claim: {userIdClaim?.Value ?? "null"}");

        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return false;
        }

        using var scope = _scopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<TUser>>();

        var appUser = await userManager.FindByIdAsync(userId.ToString());

        if (appUser is null)
        {
            return false;
        }

        return await ValidateSecurityStampAsync(userManager, user, appUser);
    }

    private async Task<bool> ValidateSecurityStampAsync(UserManager<TUser> userManager, ClaimsPrincipal principal, TUser user)
    {
        var principalStamp = principal.FindFirstValue(_options.ClaimsIdentity.SecurityStampClaimType);
        var userStamp = await userManager.GetSecurityStampAsync(user);

        return principalStamp == userStamp;
    }
}
