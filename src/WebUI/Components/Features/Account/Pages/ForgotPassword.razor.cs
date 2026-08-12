using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Vioren.CodebaseAtom.WebUI.Logics.Users.GeneratePasswordResetToken;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages;

public partial class ForgotPassword
{
    [Inject]
    public required GeneratePasswordResetTokenLogic GeneratePasswordResetTokenLogic { get; init; }

    private bool _isLoading;
    private Exception? _exception;

    private readonly ForgotPasswordModel _model = new();
    private readonly ForgotPasswordModelValidator _validator = new();
    private MudForm _form = default!;
    private MudMessageBox _messageBoxForgotPassword = default!;
    private string? _resetPasswordLink;

    private async Task HandleSubmit()
    {
        try
        {
            if (!await _form.IsValidAsync())
            {
                return;
            }

            _isLoading = true;
            _exception = null;
            _resetPasswordLink = null;

            var output = await GeneratePasswordResetTokenLogic.Handle(new GeneratePasswordResetTokenInput { Email = _model.Email });
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(output.Token));

            var callbackUrl = NavigationManager.GetUriWithQueryParameters(
                NavigationManager.ToAbsoluteUri(AccountRouteFor.ResetPassword).AbsoluteUri,
                new Dictionary<string, object?> { ["code"] = code });

            _resetPasswordLink = callbackUrl;

            _isLoading = false;

            StateHasChanged();

            _ = await _messageBoxForgotPassword.ShowAsync();
        }
        catch (Exception exception)
        {
            _exception = exception;
        }
        finally
        {
            _isLoading = false;
            await InvokeAsync(StateHasChanged);
        }
    }

    private sealed record ForgotPasswordModel
    {
        public string Email { get; set; } = string.Empty;
    }

    private sealed class ForgotPasswordModelValidator : AbstractValidatorBase<ForgotPasswordModel>
    {
        public ForgotPasswordModelValidator()
        {
            _ = RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Must(x => x.IsValidEmailAddress());
        }
    }
}
