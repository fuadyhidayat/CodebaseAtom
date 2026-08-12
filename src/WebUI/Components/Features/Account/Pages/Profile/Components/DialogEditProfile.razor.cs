using Vioren.CodebaseAtom.WebUI.Logics.Users.UpdateUser;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages.Profile.Components;

public partial class DialogEditProfile
{
    [Inject]
    public required UpdateUserLogic UpdateUserLogic { get; init; }

    [Parameter]
    public required EditProfileModel Model { get; set; }

    private readonly EditProfileModelValidator _validator = new();
    private MudForm _form = default!;

    private async Task HandleSubmit()
    {
        try
        {
            if (!await _form.IsValidAsync())
            {
                return;
            }

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
    public required string DisplayName { get; set; }
    public required string NewEmail { get; set; }
}

public sealed class EditProfileModelValidator : AbstractValidatorBase<EditProfileModel>
{
    public EditProfileModelValidator()
    {
        _ = RuleFor(x => x.DisplayName)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.User, DomainDisplayTextFor.DisplayName))
            .MaximumLength(MaximumLengthFor.Name)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.User, DomainDisplayTextFor.DisplayName, MaximumLengthFor.Name));

        _ = RuleFor(x => x.NewEmail)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.User, DomainDisplayTextFor.Email))
            .MinimumLength(MinimumLengthFor.Email)
                .WithMessage(ValidationMessageFor.MinimumLength(DomainDisplayTextFor.User, DomainDisplayTextFor.Email, MinimumLengthFor.Email))
            .MaximumLength(MaximumLengthFor.Email)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.User, DomainDisplayTextFor.Email, MaximumLengthFor.Email))
            .Must(email => string.IsNullOrEmpty(email) || email.IsValidEmailAddress())
                .WithMessage($"{DomainDisplayTextFor.User} {DomainDisplayTextFor.Email} is invalid.");
    }
}
