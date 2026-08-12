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

    private readonly ChangePasswordModelValidator _validator = new();
    private MudForm _form = default!;
    private MudMessageBox _messageBoxChangePassword = default!;

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
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}

public sealed class ChangePasswordModelValidator : AbstractValidatorBase<ChangePasswordModel>
{
    public ChangePasswordModelValidator()
    {
        _ = RuleFor(x => x.CurrentPassword)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.User, $"Current {DomainDisplayTextFor.Password}"));

        _ = RuleFor(x => x.NewPassword)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.User, $"New {DomainDisplayTextFor.Password}"))
            .MinimumLength(MinimumLengthFor.Password)
                .WithMessage(ValidationMessageFor.MinimumLength(DomainDisplayTextFor.User, $"New {DomainDisplayTextFor.Password}", MinimumLengthFor.Password))
            .MaximumLength(MaximumLengthFor.Password)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.User, $"New {DomainDisplayTextFor.Password}", MaximumLengthFor.Password));

        _ = RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.User, DomainDisplayTextFor.ConfirmPassword))
            .Equal(x => x.NewPassword)
                .WithMessage("The New Password and Confirm Password do not match.");
    }
}
