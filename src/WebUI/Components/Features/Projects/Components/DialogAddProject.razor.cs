using Vioren.CodebaseAtom.WebUI.Logics.Projects.CreateProject;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components;

public partial class DialogAddProject
{
    [Inject]
    public required CreateProjectLogic CreateProjectLogic { get; set; }

    private readonly AddProjectModel _input = new();

    private async Task OnValidSubmitAsync()
    {
        try
        {
            IsLoadingBase = true;

            var input = new CreateProjectInput
            {
                Title = _input.Title,
                Description = _input.Description
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

    private sealed record AddProjectModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
