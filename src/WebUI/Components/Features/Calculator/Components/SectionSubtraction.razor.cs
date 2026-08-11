using Vioren.CodebaseAtom.WebUI.Logics.Calculator.Subtraction;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Calculator.Components;

public partial class SectionSubtraction
{
    [Inject]
    public required SubtractionLogic SubtractionLogic { get; set; }

    private readonly SubtractionModel _subtractionModel = new();
    private int _subtractionResult;

    private async Task OnSubtractionValidSubmitAsync()
    {
        try
        {
            IsLoadingBase = true;
            ExceptionBase = null;

            var input = new SubtractionInput
            {
                Number1 = _subtractionModel.Number1,
                Number2 = _subtractionModel.Number2
            };

            var output = SubtractionLogic.Handle(input);
            _subtractionResult = output.Result;
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
}
