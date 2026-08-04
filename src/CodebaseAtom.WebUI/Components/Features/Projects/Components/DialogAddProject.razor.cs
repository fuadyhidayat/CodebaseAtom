using CodebaseAtom.WebUI.Logics.Projects.CreateProject;

namespace CodebaseAtom.WebUI.Components.Features.Projects.Components;

public partial class DialogAddProject
{
    [Inject]
    public required CreateProjectLogic CreateProjectLogic { get; set; }

    [CascadingParameter]
    private CurrentUserModel? CurrentUser { get; set; }

    private readonly AddProjectModel _input = new();

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

            var input = new CreateProjectInput
            {
                Title = _input.Title,
                Description = _input.Description,
                CreatedBy = CurrentUser.UserId
            };

            _ = await CreateProjectLogic.Handle(input);

            Snackbar.AddSuccess($"Project '{_input.Title}' has been created successfully.");

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

public sealed record AddProjectModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
