using Microsoft.AspNetCore.Identity;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Logics.Users.GeneratePasswordResetToken;

public sealed class GeneratePasswordResetTokenLogic(IServiceScopeFactory serviceScopeFactory)
{
    public async Task<GeneratePasswordResetTokenOutput> Handle(GeneratePasswordResetTokenInput input)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var applicationUser = await userManager.FindByEmailAsync(input.Email)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.User, DomainDisplayTextFor.Email, input.Email);

        var token = await userManager.GeneratePasswordResetTokenAsync(applicationUser);

        return new GeneratePasswordResetTokenOutput { Token = token };
    }
}
