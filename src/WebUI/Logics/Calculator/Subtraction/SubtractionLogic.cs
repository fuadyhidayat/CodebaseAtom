namespace Vioren.CodebaseAtom.WebUI.Logics.Calculator.Subtraction;

public sealed class SubtractionLogic
{
    public SubtractionOutput Handle(SubtractionInput input)
    {
        var result = input.Number1 - input.Number2;

        return new SubtractionOutput
        {
            Result = result
        };
    }
}
