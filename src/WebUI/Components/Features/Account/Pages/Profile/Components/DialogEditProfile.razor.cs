using System.ComponentModel.DataAnnotations;
using Vioren.CodebaseAtom.WebUI.Logics.Users.UpdateUser;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages.Profile.Components;

public partial class DialogEditProfile
{
    [Inject]
    public required UpdateUserLogic UpdateUserLogic { get; init; }

    [Parameter]
    public required EditProfileModel Model { get; set; }

    private async Task OnValidSubmitAsync()
    {
        try
        {
            IsLoadingBase = true;
            ExceptionBase = null;

            await UpdateUserLogic.Handle(new UpdateUserInput
            {
                UserId = Model.UserId,
                DisplayName = Model.DisplayName,
                Email = Model.NewEmail
            });

            Snackbar.AddSuccess("Your profile has been updated successfully.");

            Dialog.Close();
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

public sealed record EditProfileModel
{
    public required Guid UserId { get; set; }

    [Required]
    [Display(Name = "Display Name")]
    public required string DisplayName { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "New Email")]
    public required string NewEmail { get; set; }
}
