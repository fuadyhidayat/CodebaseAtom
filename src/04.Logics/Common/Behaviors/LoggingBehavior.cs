using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Vioren.CodebaseExpress.Logics.Common.Behaviors;

internal sealed partial class LoggingBehavior<TInput, TOutput>(ILogger<LoggingBehavior<TInput, TOutput>> logger)
    : ILogicBehavior<TInput, TOutput>
{
    private static readonly string _logicName = typeof(TInput).Name.Replace("Input", "Logic", StringComparison.Ordinal);

    [LoggerMessage(Level = LogLevel.Debug, Message = "Handling {logicName} with input: {input}")]
    private static partial void LogHandlingWithInput(ILogger logger, string logicName, string input);

    [LoggerMessage(Level = LogLevel.Information, Message = "Handling {logicName}")]
    private static partial void LogHandling(ILogger logger, string logicName);

    [LoggerMessage(Level = LogLevel.Information, Message = "Handled {logicName} successfully")]
    private static partial void LogHandled(ILogger logger, string logicName);

    [LoggerMessage(Level = LogLevel.Error, Message = "Error in {logicName}")]
    private static partial void LogError(ILogger logger, Exception exception, string logicName);

    public async Task<TOutput> Handle(TInput input, Func<Task<TOutput>> continuation, CancellationToken cancellationToken = default)
    {
        if (logger.IsEnabled(LogLevel.Debug))
        {
            var serializedInput = JsonSerializer.Serialize(input);
            LogHandlingWithInput(logger, _logicName, serializedInput);
        }
        else
        {
            LogHandling(logger, _logicName);
        }

        try
        {
            var result = await continuation();

            LogHandled(logger, _logicName);

            return result;
        }
        catch (Exception ex)
        {
            LogError(logger, ex, _logicName);

            throw;
        }
    }
}
