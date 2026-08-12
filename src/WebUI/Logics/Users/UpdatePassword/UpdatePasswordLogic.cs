using Microsoft.AspNetCore.Identity;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Logics.Users.UpdatePassword;

public sealed class UpdatePasswordLogic(
    IServiceScopeFactory serviceScopeFactory,
    IValidator<UpdatePasswordInput> validator)
{
    public async Task Handle(UpdatePasswordInput input, CancellationToken cancellationToken = default)
    {
        await validator.ValidateInputAsync(input, cancellationToken);

        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var applicationUser = await userManager.FindByIdAsync(input.UserId.ToString())
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.User, DomainDisplayTextFor.Id, input.UserId);

        var result = await userManager.ChangePasswordAsync(applicationUser, input.CurrentPassword, input.NewPassword);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(result.Errors.Last().Description);
        }
    }
}
