using Vioren.CodebaseAtom.WebUI.Logics.Projects.DeleteProject;
using Vioren.CodebaseAtom.WebUI.Logics.Projects.GetProject;
using Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Pages;

public partial class Details
{
    [Inject]
    public required IDialogService DialogService { get; init; }

    [Inject]
    public required GetProjectLogic GetProjectLogic { get; set; }

    [Inject]
    public required DeleteProjectLogic DeleteProjectLogic { get; set; }

    [Parameter]
    public Guid ProjectId { get; init; }

    private ProjectModel _item = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadItem();
        LoadBreadcrumbs();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(HomeBreadcrumbFor.Index);
        AddBreadcrumb(ProjectsBreadcrumbFor.Index);
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(UIDisplayTextFor.Details));
    }

    private async Task LoadItem()
    {
        try
        {
            IsLoadingBase = true;

            var output = await GetProjectLogic.Handle(new GetProjectInput
            {
                ProjectId = ProjectId
            });

            _item = new ProjectModel
            {
                Id = output.Project.Id,
                Title = output.Project.Title,
                Description = output.Project.Description,
                CreatedAt = output.Project.CreatedAt,
                CreatedBy = output.Project.CreatedBy,
                CreatedByUsername = output.Project.CreatedByUsername,
                CreatedByEmail = output.Project.CreatedByEmail,
                CreatedByDisplayName = output.Project.CreatedByDisplayName,
                UpdatedAt = output.Project.UpdatedAt,
                UpdatedBy = output.Project.UpdatedBy,
                UpdatedByUsername = output.Project.UpdatedByUsername,
                UpdatedByEmail = output.Project.UpdatedByEmail,
                UpdatedByDisplayName = output.Project.UpdatedByDisplayName
            };
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

    private async Task ShowDialogEditProject()
    {
        var model = new EditProjectModel
        {
            ProjectId = ProjectId,
            Title = _item.Title,
            Description = _item.Description
        };

        var parameters = new DialogParameters
        {
            { nameof(DialogEditProject.Model), model }
        };

        var dialog = await DialogService.ShowAsync<DialogEditProject>($"{UIDisplayTextFor.Edit} {DomainDisplayTextFor.Project}", parameters);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            await LoadItem();
        }
    }

    private async Task ShowDialogDeleteProject()
    {
        var dialogResult = await DialogService.ShowMessageBoxAsync(
          $"{UIDisplayTextFor.Delete} {DomainDisplayTextFor.Project}",
          $"Are you sure you want to {UIDisplayTextFor.Delete.ToLowerInvariant()} the {DomainDisplayTextFor.Project} '{_item.Title}' along with all its associated {DomainDisplayTextFor.WorkItems} and {DomainDisplayTextFor.Documents}?",
          yesText: UIDisplayTextFor.Yes,
          noText: UIDisplayTextFor.No,
          options: new DialogOptions { MaxWidth = MaxWidth.ExtraSmall });

        if (dialogResult is true)
        {
            try
            {
                IsLoadingBase = true;

                var input = new DeleteProjectInput
                {
                    ProjectId = _item.Id
                };

                await DeleteProjectLogic.Handle(input);

                NavigationManager.NavigateTo(ProjectsRouteFor.Index, forceLoad: true);
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
}

public sealed record ProjectModel
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required Guid CreatedBy { get; init; }
    public required string CreatedByUsername { get; init; }
    public required string CreatedByEmail { get; init; }
    public required string CreatedByDisplayName { get; init; }
    public required DateTimeOffset? UpdatedAt { get; init; }
    public required Guid? UpdatedBy { get; init; }
    public required string? UpdatedByUsername { get; init; }
    public required string? UpdatedByEmail { get; init; }
    public required string? UpdatedByDisplayName { get; init; }
}
