using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Vioren.CodebaseExpress.Logics.Common.Behaviors;

internal sealed partial class PerformanceBehavior<TInput, TOutput>(ILogger<PerformanceBehavior<TInput, TOutput>> logger)
    : ILogicBehavior<TInput, TOutput>
{
    private static readonly string _logicName = typeof(TInput).Name.Replace("Input", "Logic", StringComparison.Ordinal);
    private static readonly TimeSpan _threshold = TimeSpan.FromMilliseconds(500);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Long running logic detected: {logicName} took {elapsedMilliseconds}ms (threshold: {thresholdMilliseconds}ms)")]
    private static partial void LogLongRunning(ILogger logger, string logicName, double elapsedMilliseconds, double thresholdMilliseconds);

    public async Task<TOutput> Handle(TInput input, Func<Task<TOutput>> continuation, CancellationToken cancellationToken = default)
    {
        var start = Stopwatch.GetTimestamp();

        var result = await continuation();

        var elapsed = Stopwatch.GetElapsedTime(start);

        if (elapsed > _threshold)
        {
            LogLongRunning(logger, _logicName, elapsed.TotalMilliseconds, _threshold.TotalMilliseconds);
        }

        return result;
    }
}
