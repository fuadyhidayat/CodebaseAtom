using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using CodebaseAtom.WebUI.Infrastructure.Identity;

namespace CodebaseAtom.WebUI.Components.Features.Account.Pages.Profile.Components;

public partial class DialogChangePassword
{
    [Inject]
    public required UserManager<ApplicationUser> UserManager { get; init; }

    [Parameter]
    public required ApplicationUser ApplicationUser { get; set; }

    private readonly InputModel _input = new();

    private async Task OnValidSubmitAsync()
    {
        var result = await UserManager.ChangePasswordAsync(ApplicationUser, _input.CurrentPassword, _input.NewPassword);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                Snackbar.AddError(error.Description);
            }

            return;
        }

        Snackbar.AddSuccess("Your password has been changed successfully.");

        Dialog.Close();
    }

    private sealed record InputModel
    {
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
}

