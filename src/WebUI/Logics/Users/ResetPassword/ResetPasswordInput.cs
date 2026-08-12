namespace Vioren.CodebaseAtom.WebUI.Logics.Users.ResetPassword;

public sealed record ResetPasswordInput
{
    public required string Username { get; init; }
    public required string Token { get; init; }
    public required string NewPassword { get; init; }
}

public sealed class ResetPasswordInputValidator : AbstractValidatorBase<ResetPasswordInput>
{
    public ResetPasswordInputValidator()
    {
        _ = RuleFor(x => x.Username)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.User, DomainDisplayTextFor.Username))
            .MinimumLength(MinimumLengthFor.Username)
                .WithMessage(ValidationMessageFor.MinimumLength(DomainDisplayTextFor.User, DomainDisplayTextFor.Username, MinimumLengthFor.Username))
            .MaximumLength(MaximumLengthFor.Username)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.User, DomainDisplayTextFor.Username, MaximumLengthFor.Username));

        _ = RuleFor(x => x.Token)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.User, DomainDisplayTextFor.Code));

        _ = RuleFor(x => x.NewPassword)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.User, DomainDisplayTextFor.Password))
            .MinimumLength(MinimumLengthFor.Password)
                .WithMessage(ValidationMessageFor.MinimumLength(DomainDisplayTextFor.User, DomainDisplayTextFor.Password, MinimumLengthFor.Password))
            .MaximumLength(MaximumLengthFor.Password)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.User, DomainDisplayTextFor.Password, MaximumLengthFor.Password));
    }
}
