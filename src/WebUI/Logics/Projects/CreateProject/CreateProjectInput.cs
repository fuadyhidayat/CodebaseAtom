using Vioren.CodebaseAtom.WebUI.Common.Validators;

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
                .WithMessage($"{DomainDisplayTextFor.Project} {DomainDisplayTextFor.Title} is required.")
            .MaximumLength(3)
                .WithMessage($"Logics: The maximum length for {DomainDisplayTextFor.Project} {DomainDisplayTextFor.Title} is {MaximumLengthFor.Title} characters.");

        _ = RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage($"{DomainDisplayTextFor.Project} {DomainDisplayTextFor.Description} is required.")
            .MaximumLength(3)
                .WithMessage($"Logics: The maximum length for {DomainDisplayTextFor.Project} {DomainDisplayTextFor.Description} is {MaximumLengthFor.Description} characters.");
    }
}
