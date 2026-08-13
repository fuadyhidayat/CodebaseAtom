using Vioren.CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItem;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components.WorkItems;

public partial class DialogEditWorkItem
{
    [Inject]
    public required UpdateWorkItemLogic UpdateWorkItemLogic { get; init; }

    [Parameter]
    public required EditWorkItemModel Model { get; set; }

    private readonly EditWorkItemModelValidator _validator = new();
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

            var input = new UpdateWorkItemInput
            {
                WorkItemId = Model.WorkItemId,
                Title = Model.Title,
                Description = Model.Description,
                Deadline = Model.Deadline,
                Status = Model.Status
            };

            await UpdateWorkItemLogic.Handle(input);

            Snackbar.AddSuccess($"The {DomainDisplayTextFor.WorkItem} has been updated successfully.");

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

public sealed record EditWorkItemModel
{
    public required Guid WorkItemId { get; init; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateOnly Deadline { get; set; }
    public required WorkItemStatus Status { get; set; }

    public DateTime? DeadlineDateTime
    {
        get => Deadline.ToDateTimeNullable();
        set => Deadline = value.ToDateOnly();
    }
}

public sealed class EditWorkItemModelValidator : AbstractValidatorBase<EditWorkItemModel>
{
    public EditWorkItemModelValidator()
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

        _ = RuleFor(x => x.DeadlineDateTime)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Deadline));

        _ = RuleFor(x => x.Status)
            .IsInEnum()
                .WithMessage($"{DomainDisplayTextFor.WorkItem} {DomainDisplayTextFor.Status} is invalid.");
    }
}
