using Microsoft.AspNetCore.Identity;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Logics.Users.GetCurrentUser;

public sealed class GetCurrentUserLogic(IServiceScopeFactory serviceScopeFactory, CurrentUserService currentUserService)
{
    public async Task<GetCurrentUserOutput> Handle()
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var currentUser = await currentUserService.GetCurrentUserAsync()
            ?? throw new InvalidOperationException("Current user is not authenticated.");

        var applicationUser = await userManager.FindByIdAsync(currentUser.UserId.ToString())
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.User, DomainDisplayTextFor.Id, currentUser.UserId);

        var roles = await userManager.GetRolesAsync(applicationUser);

        var item = new UserDto
        {
            Id = applicationUser.Id,
            Username = applicationUser.UserName ?? string.Empty,
            DisplayName = applicationUser.DisplayName,
            Email = applicationUser.Email ?? string.Empty,
            Roles = roles.AsReadOnly()
        };

        return new GetCurrentUserOutput { User = item };
    }
}
