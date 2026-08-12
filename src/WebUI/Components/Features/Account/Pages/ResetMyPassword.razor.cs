using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Vioren.CodebaseAtom.WebUI.Logics.Users.ResetPassword;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages;

public partial class ResetMyPassword
{
    [Inject]
    public required IDialogService DialogService { get; init; }

    [Inject]
    public required ResetPasswordLogic ResetPasswordLogic { get; init; }

    [SupplyParameterFromQuery]
    private string? Code { get; set; }

    private bool _isLoading;
    private Exception? _exception;
    private ResetPasswordModel _model = default!;
    private readonly ResetPasswordModelValidator _validator = new();
    private MudForm _form = default!;
    private MudMessageBox _messageBoxPasswordReset = default!;

    protected override void OnInitialized()
    {
        if (string.IsNullOrWhiteSpace(Code))
        {
            _exception = new InvalidOperationException("A code must be supplied for password reset.");

            return;
        }

        try
        {
            _model = new()
            {
                Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Code))
            };
        }
        catch (Exception exception)
        {
            _exception = exception;
        }
    }

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

            await ResetPasswordLogic.Handle(new ResetPasswordInput
            {
                Username = _model.Username,
                Token = _model.Token,
                NewPassword = _model.Password
            });

            _isLoading = false;

            StateHasChanged();

            var dialogOptions = new DialogOptions
            {
                BackdropClick = false
            };

            _ = await _messageBoxPasswordReset.ShowAsync(dialogOptions);
            NavigationManager.NavigateTo(AccountRouteFor.Login(), forceLoad: true);
        }
        catch (Exception exception)
        {
            _exception = exception;
        }
        finally
        {
            _isLoading = false;

            StateHasChanged();
        }
    }

    private sealed record ResetPasswordModel
    {
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    private sealed class ResetPasswordModelValidator : AbstractValidatorBase<ResetPasswordModel>
    {
        public ResetPasswordModelValidator()
        {
            _ = RuleFor(x => x.Username)
                .NotEmpty()
                    .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Username))
                .MinimumLength(MinimumLengthFor.Username)
                    .WithMessage(ValidationMessageFor.MinimumLength(DomainDisplayTextFor.Username, MinimumLengthFor.Username))
                .MaximumLength(MaximumLengthFor.Username)
                    .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.Username, MaximumLengthFor.Username));

            _ = RuleFor(x => x.Token)
                .NotEmpty()
                    .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Code));

            _ = RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Password))
                .MinimumLength(MinimumLengthFor.Password)
                    .WithMessage(ValidationMessageFor.MinimumLength(DomainDisplayTextFor.Password, MinimumLengthFor.Password))
                .MaximumLength(MaximumLengthFor.Password)
                    .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.Password, MaximumLengthFor.Password));

            _ = RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                    .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.ConfirmPassword))
                .Equal(x => x.Password)
                    .WithMessage($"The {DomainDisplayTextFor.Password} and {DomainDisplayTextFor.ConfirmPassword} do not match.");
        }
    }
}
