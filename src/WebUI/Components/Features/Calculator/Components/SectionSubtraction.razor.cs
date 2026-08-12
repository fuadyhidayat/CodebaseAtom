using Vioren.CodebaseAtom.WebUI.Logics.Calculator.Subtraction;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Calculator.Components;

public partial class SectionSubtraction
{
    [Inject]
    public required SubtractionLogic SubtractionLogic { get; set; }

    private readonly SubtractionModel _model = new();
    private readonly SubtractionModelValidator _validator = new();
    private MudForm _form = default!;
    private int _result;

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

            var input = new SubtractionInput
            {
                Number1 = _model.Number1,
                Number2 = _model.Number2
            };

            var output = SubtractionLogic.Handle(input);
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

    private sealed record SubtractionModel
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
    }

    private sealed class SubtractionModelValidator : AbstractValidatorBase<SubtractionModel>
    {
        public SubtractionModelValidator()
        {
            _ = RuleFor(x => x.Number1)
                .NotEmpty();

            _ = RuleFor(x => x.Number2)
                .NotEmpty();
        }
    }
}
