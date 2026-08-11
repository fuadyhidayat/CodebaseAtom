namespace Vioren.CodebaseAtom.WebUI.Logics.Calculator.Addition;

public sealed class AdditionLogic
{
    public AdditionOutput Execute(AdditionInput input)
    {
        var result = input.Number1 + input.Number2;

        return new AdditionOutput
        {
            Result = result
        };
    }
}
