using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using CodebaseAtom.WebUI.Infrastructure.Identity;

namespace CodebaseAtom.WebUI.Components.Features.Account.Pages;

public partial class ForgotPassword
{
    [Inject]
    public required UserManager<ApplicationUser> UserManager { get; init; }

    [Inject]
    public required IEmailSender<ApplicationUser> EmailSender { get; init; }

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

        // For more information on how to enable account confirmation and password reset please
        // visit https://go.microsoft.com/fwlink/?LinkID=532713
        var code = await UserManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var callbackUrl = NavigationManager.GetUriWithQueryParameters(
            NavigationManager.ToAbsoluteUri(AccountRouteFor.ResetPassword).AbsoluteUri,
            new Dictionary<string, object?> { ["code"] = code });

        _resetPasswordLink = callbackUrl;

        await EmailSender.SendPasswordResetLinkAsync(user, Input.Email, HtmlEncoder.Default.Encode(callbackUrl));

        _message = "Please check your email to reset your password.";
    }

    private sealed record InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
