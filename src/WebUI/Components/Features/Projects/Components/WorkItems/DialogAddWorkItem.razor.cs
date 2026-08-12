using Vioren.CodebaseAtom.WebUI.Logics.WorkItems.CreateWorkItem;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components.WorkItems;

public partial class DialogAddWorkItem
{
    [Inject]
    public required CreateWorkItemLogic CreateWorkItemLogic { get; set; }

    [Parameter]
    public required Guid ProjectId { get; set; }

    private readonly AddWorkItemModel _model = new();
    private readonly AddWorkItemModelValidator _validator = new();
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

            var input = new CreateWorkItemInput
            {
                ProjectId = ProjectId,
                Title = _model.Title,
                Description = _model.Description,
                Deadline = _model.Deadline
            };

            _ = await CreateWorkItemLogic.Handle(input);

            Snackbar.AddSuccess($"{DomainDisplayTextFor.WorkItem} '{_model.Title}' has been created successfully.");

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

public sealed record AddWorkItemModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly Deadline { get; set; } = DateTime.Now.AddDays(7).ToDateOnly();

    public DateTime? DeadlineDateTime
    {
        get => Deadline.ToDateTimeNullable();
        set => Deadline = value.ToDateOnly();
    }
}

public sealed class AddWorkItemModelValidator : AbstractValidatorBase<AddWorkItemModel>
{
    public AddWorkItemModelValidator()
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
