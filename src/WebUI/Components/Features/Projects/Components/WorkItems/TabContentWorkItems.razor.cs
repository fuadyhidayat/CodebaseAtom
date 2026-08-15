using Vioren.CodebaseAtom.WebUI.Logics.WorkItems.DeleteWorkItem;
using Vioren.CodebaseAtom.WebUI.Logics.WorkItems.DeleteWorkItems;
using Vioren.CodebaseAtom.WebUI.Logics.WorkItems.GetWorkItems;
using Vioren.CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItem;
using Vioren.CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItemStatus;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components.WorkItems;

public partial class TabContentWorkItems
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

    [Inject]
    public required DeleteWorkItemsLogic DeleteWorkItemsLogic { get; set; }

    [Parameter, EditorRequired]
    public Guid ProjectId { get; set; }

    private string _searchKeyword = string.Empty;
    private List<WorkItemModel> _workItems = default!;
    private HashSet<WorkItemModel> _selectedWorkItems = new();
    private bool _isKanbanView;
    private MudDropContainer<WorkItemModel> _kanban = default!;
    private static WorkItemStatus[] Columns => Enum.GetValues<WorkItemStatus>();
    private IEnumerable<WorkItemModel> FilteredItems => _workItems.Where(FilterWorkItems);

    protected override async Task OnParametersSetAsync()
    {
        await LoadWorkItems();
    }

    private async Task LoadWorkItems()
    {
        try
        {
            IsLoadingBase = true;
            ExceptionBase = null;

            var output = await GetWorkItemsLogic.Handle(new GetWorkItemsInput
            {
                ProjectId = ProjectId
            });

            _workItems = output.WorkItems.Select(workItem => new WorkItemModel
            {
                Id = workItem.Id,
                Title = workItem.Title,
                Description = workItem.Description,
                Deadline = workItem.Deadline,
                Status = workItem.Status
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

    private bool FilterWorkItems(WorkItemModel workItem)
    {
        if (string.IsNullOrWhiteSpace(_searchKeyword))
        {
            return true;
        }

        if (workItem.Title.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (workItem.Description.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }

    private async Task OnSearchKeywordChanged(string value)
    {
        _searchKeyword = value;

        if (_isKanbanView && _kanban is not null)
        {
            await InvokeAsync(_kanban.Refresh);
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
            await LoadWorkItems();
        }
    }

    private async Task ShowDialogEditWorkItem(WorkItemModel workItem)
    {
        var model = new EditWorkItemModel
        {
            WorkItemId = workItem.Id,
            Title = workItem.Title,
            Description = workItem.Description,
            Deadline = workItem.Deadline,
            OriginalDeadline = workItem.Deadline,
            Status = workItem.Status
        };

        var parameters = new DialogParameters
        {
            { nameof(DialogEditWorkItem.Model), model }
        };

        var dialog = await DialogService.ShowAsync<DialogEditWorkItem>($"{UIDisplayTextFor.Edit} {DomainDisplayTextFor.WorkItem}", parameters);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            await LoadWorkItems();
        }
    }

    private async Task ShowDialogDeleteWorkItem(WorkItemModel workItem)
    {
        var dialogResult = await DialogService.ShowMessageBoxAsync(
          $"{UIDisplayTextFor.Delete} {DomainDisplayTextFor.WorkItem}",
          ConfirmationMessageFor.Delete(DomainDisplayTextFor.WorkItem, workItem.Title),
          yesText: UIDisplayTextFor.Yes,
          noText: UIDisplayTextFor.No,
          options: new DialogOptions { MaxWidth = MaxWidth.ExtraSmall });

        if (dialogResult is true)
        {
            await DeleteWorkItem(workItem);
        }
    }

    private async Task DeleteWorkItem(WorkItemModel workItem)
    {
        try
        {
            IsLoadingBase = true;
            ExceptionBase = null;

            var input = new DeleteWorkItemInput
            {
                WorkItemId = workItem.Id
            };

            await DeleteWorkItemLogic.Handle(input);
            Snackbar.AddSuccess($"{DomainDisplayTextFor.WorkItem} '{workItem.Title}' deleted successfully.");
            await LoadWorkItems();
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

    private async Task ShowDialogDeleteSelectedWorkItems()
    {
        if (_selectedWorkItems.Count == 0)
        {
            return;
        }

        var entityDisplayText = _selectedWorkItems.Count is 1 ? DomainDisplayTextFor.WorkItem : DomainDisplayTextFor.WorkItems;
        var dialogResult = await DialogService.ShowMessageBoxAsync(
          $"{UIDisplayTextFor.Delete} {DomainDisplayTextFor.WorkItems}",
          $"Are you sure you want to delete the selected {_selectedWorkItems.Count} {entityDisplayText}?",
          yesText: UIDisplayTextFor.Yes,
          noText: UIDisplayTextFor.No,
          options: new DialogOptions { MaxWidth = MaxWidth.ExtraSmall });

        if (dialogResult is true)
        {
            await DeleteWorkItems(_selectedWorkItems.Select(x => x.Id));
        }
    }

    private async Task DeleteWorkItems(IEnumerable<Guid> workItemIds)
    {
        try
        {
            IsLoadingBase = true;
            ExceptionBase = null;

            var input = new DeleteWorkItemsInput
            {
                WorkItemIds = workItemIds
            };

            await DeleteWorkItemsLogic.Handle(input);
            Snackbar.AddSuccess($"{workItemIds.Count()} {DomainDisplayTextFor.WorkItems} deleted successfully.");
            await LoadWorkItems();
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
            var index = _workItems.FindIndex(x => x.Id == workItem.Id);

            if (index is not -1)
            {
                var updatedWorkItem = _workItems[index] with { Status = newStatus };
                _workItems[index] = updatedWorkItem;

                await UpdateWorkItemStatus(updatedWorkItem);
            }
        }
    }

    private async Task UpdateWorkItemStatus(WorkItemModel workItem)
    {
        try
        {
            IsLoadingBase = true;
            ExceptionBase = null;

            var input = new UpdateWorkItemStatusInput
            {
                WorkItemId = workItem.Id,
                Status = workItem.Status
            };

            await UpdateWorkItemStatusLogic.Handle(input);
            await LoadWorkItems();
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
