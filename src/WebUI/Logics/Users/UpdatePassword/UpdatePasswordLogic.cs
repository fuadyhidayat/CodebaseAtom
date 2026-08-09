using Microsoft.AspNetCore.Identity;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Logics.Users.UpdatePassword;

public sealed class UpdatePasswordLogic(IServiceScopeFactory serviceScopeFactory, IPasswordHasher<ApplicationUser> passwordHasher)
{
    public async Task Handle(UpdatePasswordInput input)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var applicationUser = await userManager.FindByIdAsync(input.UserId.ToString())
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.User, DomainDisplayTextFor.Id, input.UserId);

        var passwordVerification = userManager.PasswordHasher.VerifyHashedPassword(applicationUser, applicationUser.PasswordHash!, input.CurrentPassword);

        if (passwordVerification is PasswordVerificationResult.Failed)
        {
            throw new InvalidOperationException("Incorrect current password.");
        }

        foreach (var validator in userManager.PasswordValidators)
        {
            var validationResult = await validator.ValidateAsync(userManager, applicationUser, input.NewPassword);

            if (!validationResult.Succeeded)
            {
                throw new AggregateException(validationResult.Errors.Select(e => new InvalidOperationException(e.Description)));
            }
        }

        applicationUser.PasswordHash = passwordHasher.HashPassword(applicationUser, input.NewPassword);

        var result = await userManager.UpdateAsync(applicationUser);

        if (!result.Succeeded)
        {
            throw new AggregateException(result.Errors.Select(e => new InvalidOperationException(e.Description)));
        }
    }
}
