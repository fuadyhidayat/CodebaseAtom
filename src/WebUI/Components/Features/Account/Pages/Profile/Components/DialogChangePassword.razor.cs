using System.ComponentModel.DataAnnotations;
using Vioren.CodebaseAtom.WebUI.Logics.Users.UpdatePassword;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages.Profile.Components;

public partial class DialogChangePassword
{
    [Inject]
    public required IDialogService DialogService { get; init; }

    [Inject]
    public required UpdatePasswordLogic UpdatePasswordLogic { get; init; }

    [Parameter]
    public required ChangePasswordModel Model { get; set; }

    private MudMessageBox _messageBoxChangePassword = default!;

    private async Task OnValidSubmitAsync()
    {
        try
        {
            IsLoadingBase = true;
            ExceptionBase = null;

            await UpdatePasswordLogic.Handle(new UpdatePasswordInput
            {
                UserId = Model.UserId,
                CurrentPassword = Model.CurrentPassword,
                NewPassword = Model.NewPassword
            });

            Dialog.Close();

            IsLoadingBase = false;

            StateHasChanged();

            var dialogOptions = new DialogOptions
            {
                BackdropClick = false
            };

            _ = await _messageBoxChangePassword.ShowAsync(dialogOptions);
            NavigationManager.NavigateTo(AccountRouteFor.Login(), forceLoad: true);
        }
        catch (Exception exception)
        {
            ExceptionBase = exception;
        }
        finally
        {
            IsLoadingBase = false;
        }
    }
}

public sealed record ChangePasswordModel
{
    public required Guid UserId { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required]
    [StringLength(MaximumLengthFor.Password, ErrorMessage = "The New Password must be at least {2} and at max {1} characters long.", MinimumLength = MinimumLengthFor.Password)]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "The New Password and Confirm Password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
