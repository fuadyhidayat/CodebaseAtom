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

    private InputModel Input { get; set; } = new();

    private MudMessageBox _messageBoxPasswordReset = default!;
    private MudForm _form = default!;
    private readonly InputModelValidator _validator = new();

    protected override void OnInitialized()
    {
        if (string.IsNullOrWhiteSpace(Code))
        {
            _exception = new InvalidOperationException("A code must be supplied for password reset.");

            return;
        }

        try
        {
            Input.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Code));
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
                Username = Input.Username,
                Token = Input.Code,
                NewPassword = Input.Password
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

    private sealed record InputModel
    {
        public string Username { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    private sealed class InputModelValidator : AbstractValidatorBase<InputModel>
    {
        public InputModelValidator()
        {
            _ = RuleFor(x => x.Username)
                .NotEmpty()
                    .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.User, DomainDisplayTextFor.Username))
                .MinimumLength(MinimumLengthFor.Username)
                    .WithMessage(ValidationMessageFor.MinimumLength(DomainDisplayTextFor.User, DomainDisplayTextFor.Username, MinimumLengthFor.Username))
                .MaximumLength(MaximumLengthFor.Username)
                    .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.User, DomainDisplayTextFor.Username, MaximumLengthFor.Username));

            _ = RuleFor(x => x.Code)
                .NotEmpty()
                    .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.User, DomainDisplayTextFor.Code));

            _ = RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.User, DomainDisplayTextFor.Password))
                .MinimumLength(MinimumLengthFor.Password)
                    .WithMessage(ValidationMessageFor.MinimumLength(DomainDisplayTextFor.User, DomainDisplayTextFor.Password, MinimumLengthFor.Password))
                .MaximumLength(MaximumLengthFor.Password)
                    .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.User, DomainDisplayTextFor.Password, MaximumLengthFor.Password));

            _ = RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                    .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.User, DomainDisplayTextFor.ConfirmPassword))
                .Equal(x => x.Password)
                    .WithMessage($"The {DomainDisplayTextFor.Password} and {DomainDisplayTextFor.ConfirmPassword} do not match.");
        }
    }
}
