using System.Diagnostics;

namespace BehaviorConsoleApp;

public sealed partial class PerformanceBehavior<TInput, TOutput>()
    : ILogicBehavior<TInput, TOutput>
{
    private static readonly TimeSpan _threshold = TimeSpan.FromMilliseconds(500);
    private static readonly string _logicName = typeof(TInput).Name.Replace("Input", "", StringComparison.Ordinal);

    public async Task<TOutput> Handle(TInput input, Func<Task<TOutput>> continuation, CancellationToken cancellationToken = default)
    {
        var start = Stopwatch.GetTimestamp();
        Console.WriteLine($"PERFORMANCE:\tStarting {_logicName}");
        var result = await continuation();
        Console.WriteLine($"PERFORMANCE:\tFinished {_logicName}");
        var elapsed = Stopwatch.GetElapsedTime(start);

        if (elapsed > _threshold)
        {
            Console.WriteLine($"PERFORMANCE:\tLong running logic detected: {_logicName} took {elapsed.TotalMilliseconds}ms (threshold: {_threshold.TotalMilliseconds}ms)");
        }

        return result;
    }
}
