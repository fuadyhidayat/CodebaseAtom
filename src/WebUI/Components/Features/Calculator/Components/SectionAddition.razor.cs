using Vioren.CodebaseAtom.WebUI.Logics.Calculator.Addition;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Calculator.Components;

public partial class SectionAddition
{
    [Inject]
    public required AdditionLogic AdditionLogic { get; set; }

    private readonly AdditionModel _additionModel = new();
    private int _additionResult;

    private async Task OnAdditionValidSubmitAsync()
    {
        try
        {
            IsLoadingBase = true;
            ExceptionBase = null;

            var input = new AdditionInput
            {
                Number1 = _additionModel.Number1,
                Number2 = _additionModel.Number2
            };

            var output = AdditionLogic.Handle(input);
            _additionResult = output.Result;
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
}
