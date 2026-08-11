namespace Vioren.CodebaseAtom.WebUI.Logics.Calculator.Addition;

public sealed record AdditionInput
{
    public required int Number1 { get; init; }
    public required int Number2 { get; init; }
}
