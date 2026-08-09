using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

public class ApplicationUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IOptions<IdentityOptions> identityOptionsProvider)
    : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>(userManager, roleManager, identityOptionsProvider)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        identity.AddClaim(new Claim(ClaimTypeFor.DisplayName, user.DisplayName));

        return identity;
    }
}
