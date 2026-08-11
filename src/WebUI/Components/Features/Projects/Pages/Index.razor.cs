using Vioren.CodebaseAtom.WebUI.Logics.Projects.GetProjects;
using Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Pages;

public partial class Index
{
    [Inject]
    public required IDialogService DialogService { get; init; }

    [Inject]
    public required GetProjectsLogic GetProjectsLogic { get; set; }

    private string _searchKeyword = string.Empty;

    private IReadOnlyList<ProjectModel> _projects = default!;

    protected override async Task OnInitializedAsync()
    {
        LoadBreadcrumbs();
        await LoadProjects();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(HomeBreadcrumbFor.Index);
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(DomainDisplayTextFor.Projects));
    }

    private async Task LoadProjects()
    {
        try
        {
            IsLoadingBase = true;

            var output = await GetProjectsLogic.Handle();

            _projects = output.Projects.Select(project => new ProjectModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description
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

    private bool FilterItems(ProjectModel project)
    {
        if (string.IsNullOrWhiteSpace(_searchKeyword))
        {
            return true;
        }

        if (project.Title.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (project.Description.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase))
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
            await LoadProjects();
        }
    }

    private sealed record ProjectModel
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Description { get; init; }
    }
}
