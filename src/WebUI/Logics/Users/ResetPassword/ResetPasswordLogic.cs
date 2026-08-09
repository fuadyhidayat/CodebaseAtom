using Microsoft.AspNetCore.Identity;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Logics.Users.ResetPassword;

public sealed class ResetPasswordLogic(IServiceScopeFactory serviceScopeFactory)
{
    public async Task Handle(ResetPasswordInput input)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var applicationUser = await userManager.FindByNameAsync(input.Username)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.User, DomainDisplayTextFor.Username, input.Username);

        var result = await userManager.ResetPasswordAsync(applicationUser, input.Code, input.Password);

        if (!result.Succeeded)
        {
            throw new AggregateException(result.Errors.Select(e => new InvalidOperationException(e.Description)));
        }
    }
}
