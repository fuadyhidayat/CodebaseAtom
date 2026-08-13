namespace BehaviorConsoleApp;

public sealed class AdditionLogic() : ILogic<AdditionInput, AdditionOutput>
{
    public async Task<AdditionOutput> Handle(AdditionInput input, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"ADDITION LOGIC:\tAdding {input.Number1} and {input.Number2}...");

        var result = input.Number1 + input.Number2;

        await Task.Delay(600, cancellationToken);
        await Task.CompletedTask;

        var output = new AdditionOutput
        {
            Result = result
        };

        return output;
    }
}

public sealed record AdditionInput
{
    public required int Number1 { get; init; }
    public required int Number2 { get; init; }
}

public sealed record AdditionOutput
{
    public required int Result { get; init; }
}
