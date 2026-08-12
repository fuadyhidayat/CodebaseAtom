using Vioren.CodebaseAtom.WebUI.Logics.Projects.CreateProject;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components;

public partial class DialogAddProject
{
    [Inject]
    public required CreateProjectLogic CreateProjectLogic { get; set; }

    private readonly AddProjectModel _model = new();
    private readonly AddProjectModelValidator _validator = new();
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

            var input = new CreateProjectInput
            {
                Title = _model.Title,
                Description = _model.Description
            };

            var output = await CreateProjectLogic.Handle(input);

            Snackbar.AddSuccess($"Project '{_model.Title}' has been created successfully.");

            Dialog.Close(output.ProjectId);
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

    private sealed class AddProjectModelValidator : AbstractValidatorBase<AddProjectModel>
    {
        public AddProjectModelValidator()
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
}
