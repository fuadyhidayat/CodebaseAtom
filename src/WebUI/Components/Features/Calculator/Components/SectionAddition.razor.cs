using Vioren.CodebaseAtom.WebUI.Logics.Calculator.Addition;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Calculator.Components;

public partial class SectionAddition
{
    [Inject]
    public required AdditionLogic AdditionLogic { get; set; }

    private readonly AdditionModel _model = new();
    private readonly AdditionModelValidator _validator = new();
    private MudForm _form = default!;
    private int _result;

    private async Task OnAdditionValidSubmitAsync()
    {
        try
        {
            if (!await _form.IsValidAsync())
            {
                return;
            }

            IsLoadingBase = true;
            ExceptionBase = null;

            var input = new AdditionInput
            {
                Number1 = _model.Number1,
                Number2 = _model.Number2
            };

            var output = AdditionLogic.Handle(input);
            _result = output.Result;
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

    private sealed record AdditionModel
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
    }

    private sealed class AdditionModelValidator : AbstractValidatorBase<AdditionModel>
    {
        public AdditionModelValidator()
        {
            _ = RuleFor(x => x.Number1)
                .NotEmpty();

            _ = RuleFor(x => x.Number2)
                .NotEmpty();
        }
    }
}
