using System.ComponentModel.DataAnnotations;
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

    private async Task OnValidSubmitAsync()
    {
        try
        {
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
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(MaximumLengthFor.Password, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = MinimumLengthFor.Password)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = DomainDisplayTextFor.ConfirmPassword)]
        [Compare(DomainDisplayTextFor.Password, ErrorMessage = $"The {DomainDisplayTextFor.Password} and {DomainDisplayTextFor.ConfirmPassword} do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
