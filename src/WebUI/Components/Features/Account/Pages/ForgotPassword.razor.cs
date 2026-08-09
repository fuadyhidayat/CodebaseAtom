using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Vioren.CodebaseAtom.WebUI.Logics.Users.GeneratePasswordResetToken;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages;

public partial class ForgotPassword
{
    [Inject]
    public required GeneratePasswordResetTokenLogic GeneratePasswordResetTokenLogic { get; init; }

    private string? _resetPasswordLink;
    private bool _isLoading;
    private Exception? _exception;

    private InputModel Input { get; set; } = new();

    private async Task OnValidSubmitAsync()
    {
        try
        {
            _isLoading = true;
            _exception = null;
            _resetPasswordLink = null;

            var output = await GeneratePasswordResetTokenLogic.Handle(new GeneratePasswordResetTokenInput { Email = Input.Email });
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(output.Token));

            var callbackUrl = NavigationManager.GetUriWithQueryParameters(
                NavigationManager.ToAbsoluteUri(AccountRouteFor.ResetPassword).AbsoluteUri,
                new Dictionary<string, object?> { ["code"] = code });

            _resetPasswordLink = callbackUrl;
        }
        catch (Exception exception)
        {
            _exception = exception;
        }
        finally
        {
            _isLoading = false;
        }
    }

    private sealed record InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
