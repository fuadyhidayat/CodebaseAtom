using Microsoft.AspNetCore.Identity;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Logics.Users.GetUser;

public sealed class GetUserLogic(IServiceScopeFactory serviceScopeFactory)
{
    public async Task<GetUserOutput> Handle(GetUserInput input)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var applicationUser = await userManager.FindByIdAsync(input.Id.ToString())
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.User, DomainDisplayTextFor.Id, input.Id);

        var roles = await userManager.GetRolesAsync(applicationUser);

        var item = new UserDto
        {
            Id = applicationUser.Id,
            Username = applicationUser.UserName ?? string.Empty,
            DisplayName = applicationUser.DisplayName,
            Email = applicationUser.Email ?? string.Empty,
            Roles = roles.AsReadOnly()
        };

        return new GetUserOutput { Item = item };
    }
}
