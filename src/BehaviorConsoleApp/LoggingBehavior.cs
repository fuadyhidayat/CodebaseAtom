namespace BehaviorConsoleApp;

public sealed partial class LoggingBehavior<TInput, TOutput>()
    : ILogicBehavior<TInput, TOutput>
{
    private static readonly string _inputName = typeof(TInput).Name;
    private static readonly string _logicName = _inputName.Replace("Input", "", StringComparison.Ordinal);

    public async Task<TOutput> Handle(TInput input, Func<Task<TOutput>> continuation, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"LOGGING:\tStarting {_logicName}");
        Console.WriteLine($"\t\t{input}");

        var result = await continuation();

        Console.WriteLine($"LOGGING:\tFinished {_logicName}");

        return result;
    }
}
