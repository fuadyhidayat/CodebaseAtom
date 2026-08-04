namespace Vioren.CodebaseExpress.Logics.Common;

/// <summary>
/// Defines a pipeline behavior that wraps a logic handler. Behaviors are executed in registration order.
/// </summary>
public interface ILogicBehavior<TInput, TOutput>
{
    public Task<TOutput> Handle(TInput input, Func<Task<TOutput>> continuation, CancellationToken cancellationToken = default);
}
