using Microsoft.AspNetCore.Identity;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.CurrentUser;

public class CurrentUserService(UserManager<ApplicationUser> userManager)
{
    public async Task<CurrentUserModel?> GetCurrentUserAsync(Guid userId)
    {
        var applicationUser = await userManager.FindByIdAsync(userId.ToString());

        if (applicationUser is null)
        {
            return null;
        }

        var roles = await userManager.GetRolesAsync(applicationUser);

        return new CurrentUserModel
        {
            UserId = applicationUser.Id,
            Username = applicationUser.UserName ?? "unknown",
            DisplayName = applicationUser.DisplayName,
            Roles = roles.ToList()
        };
    }
}
