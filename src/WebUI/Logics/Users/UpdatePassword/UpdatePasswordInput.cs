namespace Vioren.CodebaseAtom.WebUI.Logics.Users.UpdatePassword;

public sealed record UpdatePasswordInput
{
    public required Guid UserId { get; init; }
    public required string CurrentPassword { get; init; }
    public required string NewPassword { get; init; }
}

public sealed class UpdatePasswordInputValidator : AbstractValidatorBase<UpdatePasswordInput>
{
    public UpdatePasswordInputValidator()
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
    }
}
