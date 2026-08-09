using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages.Profile.Components;

public partial class DialogEditProfile
{
    [Inject]
    public required UserManager<ApplicationUser> UserManager { get; init; }

    [Parameter]
    public required ApplicationUser ApplicationUser { get; set; }

    private InputModel _input = default!;

    protected override void OnParametersSet()
    {
        _input = new InputModel
        {
            DisplayName = ApplicationUser.DisplayName,
            NewEmail = ApplicationUser.Email ?? string.Empty
        };
    }

    private async Task OnValidSubmitAsync()
    {
        var somethingChanged = false;

        if (ApplicationUser.DisplayName != _input.DisplayName)
        {
            ApplicationUser.DisplayName = _input.DisplayName;

            var result = await UserManager.UpdateAsync(ApplicationUser);

            if (!result.Succeeded)
            {
                ExceptionBase = new AggregateException(result.Errors.Select(e => new InvalidOperationException(e.Description)));

                return;
            }

            somethingChanged = true;
        }

        if (ApplicationUser.Email != _input.NewEmail)
        {
            var code = await UserManager.GenerateChangeEmailTokenAsync(ApplicationUser, _input.NewEmail);
            var result = await UserManager.ChangeEmailAsync(ApplicationUser, _input.NewEmail, code);

            if (!result.Succeeded)
            {
                ExceptionBase = new AggregateException(result.Errors.Select(e => new InvalidOperationException(e.Description)));

                return;
            }

            somethingChanged = true;
        }

        if (somethingChanged)
        {
            Snackbar.AddSuccess("Your profile has been updated successfully.");
        }

        Dialog.Close();
    }

    private sealed record InputModel
    {
        [Required]
        [Display(Name = "Display Name")]
        public required string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "New Email")]
        public required string NewEmail { get; set; }
    }
}
