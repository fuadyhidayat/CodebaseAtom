namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItem;

public sealed record UpdateWorkItemInput
{
    public required Guid WorkItemId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateOnly Deadline { get; init; }
    public required WorkItemStatus Status { get; init; }
}

public sealed class UpdateWorkItemInputValidator : AbstractValidatorBase<UpdateWorkItemInput>
{
    public UpdateWorkItemInputValidator()
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

        _ = RuleFor(x => x.Status)
            .IsInEnum()
                .WithMessage($"{DomainDisplayTextFor.WorkItem} {DomainDisplayTextFor.Status} is invalid.");
    }
}
