namespace Vioren.CodebaseAtom.WebUI.Logics.Projects.CreateProject;

public sealed record CreateProjectInput
{
    public required string Title { get; init; }
    public required string Description { get; init; }
}

public sealed class CreateProjectInputValidator : AbstractValidatorBase<CreateProjectInput>
{
    public CreateProjectInputValidator()
    {
        _ = RuleFor(x => x.Title)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Project, DomainDisplayTextFor.Title))
            .MaximumLength(MaximumLengthFor.Title)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.Project, DomainDisplayTextFor.Title, MaximumLengthFor.Title));

        _ = RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Project, DomainDisplayTextFor.Description))
            .MaximumLength(MaximumLengthFor.Description)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.Project, DomainDisplayTextFor.Description, MaximumLengthFor.Description));
    }
}
