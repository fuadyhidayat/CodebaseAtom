using Microsoft.AspNetCore.Identity;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Logics.Users.UpdateUser;

public sealed class UpdateUserLogic(IServiceScopeFactory serviceScopeFactory)
{
    public async Task Handle(UpdateUserInput input)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var applicationUser = await userManager.FindByIdAsync(input.UserId.ToString())
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.User, DomainDisplayTextFor.Id, input.UserId);

        applicationUser.DisplayName = input.DisplayName;
        applicationUser.Email = input.Email;
        applicationUser.EmailConfirmed = true;

        var result = await userManager.UpdateAsync(applicationUser);

        if (!result.Succeeded)
        {
            throw new AggregateException(result.Errors.Select(e => new InvalidOperationException(e.Description)));
        }
    }
}
