using System.ComponentModel.DataAnnotations;
using System.Text;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages;

public partial class ForgotPassword
{
    [Inject]
    public required UserManager<ApplicationUser> UserManager { get; init; }

    private string? _message;
    private string? _resetPasswordLink;

    private InputModel Input { get; set; } = new();

    private async Task OnValidSubmitAsync()
    {
        _resetPasswordLink = null;

        var user = await UserManager.FindByEmailAsync(Input.Email);

        if (user is null || !await UserManager.IsEmailConfirmedAsync(user))
        {
            // Don't reveal that the user does not exist or is not confirmed
            _message = "Please check your email to reset your password.";

            return;
        }

        var code = await UserManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var callbackUrl = NavigationManager.GetUriWithQueryParameters(
            NavigationManager.ToAbsoluteUri(AccountRouteFor.ResetPassword).AbsoluteUri,
            new Dictionary<string, object?> { ["code"] = code });

        _resetPasswordLink = callbackUrl;
    }

    private sealed record InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
