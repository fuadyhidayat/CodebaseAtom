namespace Vioren.CodebaseAtom.WebUI.Logics.Users.UpdateUser;

public sealed record UpdateUserInput
{
    public required Guid UserId { get; init; }
    public required string Email { get; init; }
    public required string DisplayName { get; init; }
}

public sealed class UpdateUserInputValidator : AbstractValidatorBase<UpdateUserInput>
{
    public UpdateUserInputValidator()
    {
        _ = RuleFor(x => x.Email)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.User, DomainDisplayTextFor.Email))
            .MinimumLength(MinimumLengthFor.Email)
                .WithMessage(ValidationMessageFor.MinimumLength(DomainDisplayTextFor.User, DomainDisplayTextFor.Email, MinimumLengthFor.Email))
            .MaximumLength(MaximumLengthFor.Email)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.User, DomainDisplayTextFor.Email, MaximumLengthFor.Email))
            .Must(email => string.IsNullOrEmpty(email) || email.IsValidEmailAddress())
                .WithMessage($"{DomainDisplayTextFor.User} {DomainDisplayTextFor.Email} is invalid.");

        _ = RuleFor(x => x.DisplayName)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.User, DomainDisplayTextFor.DisplayName))
            .MaximumLength(MaximumLengthFor.Name)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.User, DomainDisplayTextFor.DisplayName, MaximumLengthFor.Name));
    }
}
