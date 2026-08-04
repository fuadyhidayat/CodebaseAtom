using CodebaseAtom.WebUI.Logics.Projects.GetProjects;
using CodebaseAtom.WebUI.Components.Features.Projects.Components;

namespace CodebaseAtom.WebUI.Components.Features.Projects.Pages;

public partial class Index
{
    [Inject]
    public required IDialogService DialogService { get; init; }

    [Inject]
    public required ILogic<GetProjectsInput, GetProjectsOutput> GetProjectsLogic { get; set; }

    private string _searchKeyword = string.Empty;

    private IReadOnlyList<ProjectModel> _items = default!;

    protected override async Task OnInitializedAsync()
    {
        LoadBreadcrumbs();
        await LoadItems();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(HomeBreadcrumbFor.Index);
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(DomainDisplayTextFor.Projects));
    }

    private async Task LoadItems()
    {
        try
        {
            IsLoadingBase = true;

            var output = await GetProjectsLogic.Handle(new GetProjectsInput());

            _items = output.Items.Select(item => new ProjectModel
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description
            }).ToList();
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

    private bool FilterItems(ProjectModel item)
    {
        if (string.IsNullOrWhiteSpace(_searchKeyword))
        {
            return true;
        }

        if (item.Title.Contains(_searchKeyword))
        {
            return true;
        }

        if (item.Description.Contains(_searchKeyword))
        {
            return true;
        }

        return false;
    }

    private async Task ShowDialogAddProject()
    {
        var dialog = await DialogService.ShowAsync<DialogAddProject>($"{UIDisplayTextFor.Add} {DomainDisplayTextFor.Project}");
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            await LoadItems();
        }
    }

    private sealed record ProjectModel
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Description { get; init; }
    }
}
