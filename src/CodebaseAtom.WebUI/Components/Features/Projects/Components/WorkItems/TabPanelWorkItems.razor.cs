using CodebaseAtom.WebUI.Common.Models;
using CodebaseAtom.WebUI.Logics.WorkItems.DeleteWorkItem;
using CodebaseAtom.WebUI.Logics.WorkItems.GetWorkItems;
using CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItem;
using CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItemStatus;

namespace CodebaseAtom.WebUI.Components.Features.Projects.Components.WorkItems;

public partial class TabPanelWorkItems
{
    [Inject]
    public required IDialogService DialogService { get; init; }

    [Inject]
    public required GetWorkItemsLogic GetWorkItemsLogic { get; set; }

    [Inject]
    public required UpdateWorkItemLogic UpdateWorkItemLogic { get; set; }

    [Inject]
    public required UpdateWorkItemStatusLogic UpdateWorkItemStatusLogic { get; set; }

    [Inject]
    public required DeleteWorkItemLogic DeleteWorkItemLogic { get; set; }

    [CascadingParameter]
    private CurrentUser? CurrentUser { get; set; }

    [Parameter, EditorRequired]
    public Guid ProjectId { get; set; }

    private MudDropContainer<WorkItemModel> _kanban = default!;
    private static WorkItemStatus[] Columns => Enum.GetValues<WorkItemStatus>();
    private List<WorkItemModel> _items = new();
    private bool _isKanbanView = true;

    protected override async Task OnParametersSetAsync()
    {
        await LoadItems();
    }

    private async Task LoadItems()
    {
        try
        {
            IsLoadingBase = true;

            var output = await GetWorkItemsLogic.Handle(new GetWorkItemsInput
            {
                ProjectId = ProjectId
            });

            _items = output.Items.Select(item => new WorkItemModel
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Deadline = item.Deadline,
                Status = item.Status
            }).ToList();

            await InvokeAsync(StateHasChanged);
            await InvokeAsync(_kanban.Refresh);
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

    private async Task ShowDialogAddWorkItem()
    {
        var parameters = new DialogParameters
        {
            { nameof(DialogAddWorkItem.ProjectId), ProjectId }
        };

        var dialog = await DialogService.ShowAsync<DialogAddWorkItem>($"{UIDisplayTextFor.Add} {DomainDisplayTextFor.WorkItem}", parameters);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            await LoadItems();
        }
    }

    private async Task ShowDialogEditWorkItem(WorkItemModel item)
    {
        var model = new EditWorkItemModel
        {
            WorkItemId = item.Id,
            Title = item.Title,
            Description = item.Description,
            Deadline = item.Deadline
        };

        var parameters = new DialogParameters
        {
            { nameof(DialogEditWorkItem.Model), model }
        };

        var dialog = await DialogService.ShowAsync<DialogEditWorkItem>($"{UIDisplayTextFor.Edit} {DomainDisplayTextFor.WorkItem}", parameters);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            await LoadItems();
        }
    }

    private async Task ShowDialogDeleteWorkItem(WorkItemModel item)
    {
        var dialogResult = await DialogService.ShowMessageBoxAsync(
          $"{UIDisplayTextFor.Delete} {DomainDisplayTextFor.WorkItem}",
          ConfirmationMessageFor.Delete(DomainDisplayTextFor.WorkItem, item.Title),
          yesText: UIDisplayTextFor.Yes,
          noText: UIDisplayTextFor.No,
          options: new DialogOptions { MaxWidth = MaxWidth.ExtraSmall });

        if (dialogResult is true)
        {
            await DeleteWorkItem(item.Id);
        }
    }

    private async Task DeleteWorkItem(Guid workItemId)
    {
        try
        {
            IsLoadingBase = true;

            var input = new DeleteWorkItemInput
            {
                WorkItemId = workItemId
            };

            await DeleteWorkItemLogic.Handle(input);
            await LoadItems();
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

    private async Task HandleItemDropped(MudItemDropInfo<WorkItemModel> dropInfo)
    {
        if (dropInfo.Item is null)
        {
            return;
        }

        var workItem = dropInfo.Item;

        if (Enum.TryParse<WorkItemStatus>(dropInfo.DropzoneIdentifier, out var newStatus) && workItem.Status != newStatus)
        {
            var index = _items.FindIndex(x => x.Id == workItem.Id);

            if (index is not -1)
            {
                var updatedWorkItem = _items[index] with { Status = newStatus };
                _items[index] = updatedWorkItem;

                await UpdateWorkItemStatus(updatedWorkItem);
            }
        }
    }

    private async Task UpdateWorkItemStatus(WorkItemModel workItem)
    {
        if (CurrentUser is null)
        {
            NavigationManager.NavigateTo(AccountRouteFor.Login(), forceLoad: true);

            return;
        }

        try
        {
            IsLoadingBase = true;

            var input = new UpdateWorkItemStatusInput
            {
                WorkItemId = workItem.Id,
                NewStatus = workItem.Status,
                ModifiedBy = CurrentUser.UserId
            };

            await UpdateWorkItemStatusLogic.Handle(input);
            await LoadItems();
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

    private sealed record WorkItemModel
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Description { get; init; }
        public required DateOnly Deadline { get; init; }
        public required WorkItemStatus Status { get; init; }
    }
}
