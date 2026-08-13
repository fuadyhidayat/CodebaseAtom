namespace BehaviorConsoleApp;

public interface ILogicBehavior<TInput, TOutput>
{
    public Task<TOutput> Handle(TInput input, Func<Task<TOutput>> continuation, CancellationToken cancellationToken = default);
}
