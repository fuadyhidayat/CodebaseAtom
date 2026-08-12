using Vioren.CodebaseAtom.WebUI.Logics.Projects.UpdateProject;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components;

public partial class DialogEditProject
{
    [Inject]
    public required UpdateProjectLogic UpdateProjectLogic { get; init; }

    [Parameter]
    public required EditProjectModel Model { get; set; }

    private readonly EditProjectModelValidator _validator = new();
    private MudForm _form = default!;

    private async Task HandleSubmit()
    {
        try
        {
            if (!await _form.IsValidAsync())
            {
                return;
            }

            IsLoadingBase = true;
            ExceptionBase = null;

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
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public sealed class EditProjectModelValidator : AbstractValidatorBase<EditProjectModel>
{
    public EditProjectModelValidator()
    {
        _ = RuleFor(x => x.Title)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Project, DomainDisplayTextFor.Title))
            .MaximumLength(MaximumLengthFor.Title)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.Project, DomainDisplayTextFor.Title, MaximumLengthFor.Title));

        _ = RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage(ValidationMessageFor.Required(DomainDisplayTextFor.Project, DomainDisplayTextFor.Description))
            .MaximumLength(MaximumLengthFor.Description)
                .WithMessage(ValidationMessageFor.MaximumLength(DomainDisplayTextFor.Project, DomainDisplayTextFor.Description, MaximumLengthFor.Description));
    }
}
