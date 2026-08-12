using Vioren.CodebaseAtom.WebUI.Common.Validators;
using Vioren.CodebaseAtom.WebUI.Logics.Projects.CreateProject;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components;

public partial class DialogAddProject
{
    [Inject]
    public required CreateProjectLogic CreateProjectLogic { get; set; }

    private MudForm _form = default!;
    private readonly AddProjectModel _model = new();
    private readonly AddProjectModelValidator _validator = new();

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
                    .WithMessage($"{DomainDisplayTextFor.Project} {DomainDisplayTextFor.Title} is required.")
                .MaximumLength(5)
                    .WithMessage($"Blazor: The maximum length for {DomainDisplayTextFor.Project} {DomainDisplayTextFor.Title} is {MaximumLengthFor.Title} characters.");

            _ = RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage($"{DomainDisplayTextFor.Project} {DomainDisplayTextFor.Description} is required.")
                .MaximumLength(5)
                    .WithMessage($"Blazor: The maximum length for {DomainDisplayTextFor.Project} {DomainDisplayTextFor.Description} is {MaximumLengthFor.Description} characters.");
        }
    }
}
