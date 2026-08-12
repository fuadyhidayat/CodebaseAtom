using Microsoft.AspNetCore.Identity;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Logics.Users.ResetPassword;

public sealed class ResetPasswordLogic(
    IServiceScopeFactory serviceScopeFactory,
    IValidator<ResetPasswordInput> validator)
{
    public async Task Handle(ResetPasswordInput input, CancellationToken cancellationToken = default)
    {
        await validator.ValidateInputAsync(input, cancellationToken);

        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var applicationUser = await userManager.FindByNameAsync(input.Username)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.User, DomainDisplayTextFor.Username, input.Username);

        var result = await userManager.ResetPasswordAsync(applicationUser, input.Token, input.NewPassword);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(result.Errors.Last().Description);
        }
    }
}
