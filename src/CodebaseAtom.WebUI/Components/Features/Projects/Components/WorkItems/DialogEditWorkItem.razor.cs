using CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItem;

namespace CodebaseAtom.WebUI.Components.Features.Projects.Components.WorkItems;

public partial class DialogEditWorkItem
{
    [Inject]
    public required ILogic<UpdateWorkItemInput, Unit> UpdateWorkItemLogic { get; init; }

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
                Deadline = Model.Deadline
            };

            _ = await UpdateWorkItemLogic.Handle(input);

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
    public Guid WorkItemId { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly Deadline { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddDays(7));
}
