using System.ComponentModel.DataAnnotations;
using Vioren.CodebaseAtom.WebUI.Logics.Projects.UpdateProject;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components;

public partial class DialogEditProject
{
    [Inject]
    public required UpdateProjectLogic UpdateProjectLogic { get; init; }

    [Parameter]
    public required EditProjectModel Model { get; set; }

    private async Task OnValidSubmitAsync()
    {
        try
        {
            IsLoadingBase = true;

            var input = new UpdateProjectInput
            {
                ProjectId = Model.ProjectId,
                Title = Model.Title,
                Description = Model.Description
            };

            await UpdateProjectLogic.Handle(input);

            Snackbar.AddSuccess($"The {DomainDisplayTextFor.Project} has been updated successfully.");

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

public sealed record EditProjectModel
{
    public Guid ProjectId { get; init; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(MaximumLengthFor.Title, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(MaximumLengthFor.Description, ErrorMessage = "Description cannot exceed 2000 characters.")]
    public string Description { get; set; } = string.Empty;
}
