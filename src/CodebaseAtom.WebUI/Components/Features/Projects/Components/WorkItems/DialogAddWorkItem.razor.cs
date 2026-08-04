using CodebaseAtom.WebUI.Common.Models;
using CodebaseAtom.WebUI.Logics.WorkItems.CreateWorkItem;

namespace CodebaseAtom.WebUI.Components.Features.Projects.Components.WorkItems;

public partial class DialogAddWorkItem
{
    [Inject]
    public required CreateWorkItemLogic CreateWorkItemLogic { get; set; }

    [CascadingParameter]
    private CurrentUser? CurrentUser { get; set; }

    [Parameter]
    public required Guid ProjectId { get; set; }

    private readonly AddWorkItemModel _input = new();

    private DateTime? DeadlineDateTime
    {
        get => _input.Deadline.ToDateTimeNullable();
        set => _input.Deadline = value.ToDateOnly();
    }

    private async Task OnValidSubmitAsync()
    {
        if (CurrentUser is null)
        {
            NavigationManager.NavigateTo(AccountRouteFor.Login(), forceLoad: true);

            return;
        }

        try
        {
            IsLoadingBase = true;

            var input = new CreateWorkItemInput
            {
                ProjectId = ProjectId,
                Title = _input.Title,
                Description = _input.Description,
                Deadline = _input.Deadline,
                CreatedBy = CurrentUser.UserId
            };

            _ = await CreateWorkItemLogic.Handle(input);

            Snackbar.AddSuccess($"{DomainDisplayTextFor.WorkItem} '{_input.Title}' has been created successfully.");

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
}
