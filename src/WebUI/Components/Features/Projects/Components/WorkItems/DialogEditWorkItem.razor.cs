using Vioren.CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItem;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components.WorkItems;

public partial class DialogEditWorkItem
{
    [Inject]
    public required UpdateWorkItemLogic UpdateWorkItemLogic { get; init; }

    [Parameter]
    public required EditWorkItemModel Model { get; set; }

    private DateTime? DeadlineDateTime
    {
        get => Model.Deadline.ToDateTimeNullable();
        set => Model.Deadline = value.ToDateOnly();
    }

    private async Task OnValidSubmitAsync()
    {
        try
        {
            IsLoadingBase = true;

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
}
