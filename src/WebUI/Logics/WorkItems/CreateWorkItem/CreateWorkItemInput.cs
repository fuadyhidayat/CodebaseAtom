namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.CreateWorkItem;

public sealed record CreateWorkItemInput
{
    public required Guid ProjectId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateOnly Deadline { get; init; }
}

public sealed class CreateWorkItemInputValidator : AbstractValidatorBase<CreateWorkItemInput>
{
    public CreateWorkItemInputValidator()
    {
        _ = RuleFor(x => x.Title)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Title))
            .MaximumLength(MaximumLengthFor.Title)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Title, MaximumLengthFor.Title));

        _ = RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Description))
            .MaximumLength(MaximumLengthFor.Description)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Description, MaximumLengthFor.Description));

        _ = RuleFor(x => x.Deadline)
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage($"{DomainDisplayTextFor.WorkItem} {DomainDisplayTextFor.Deadline} cannot be in the past.");
    }
}
