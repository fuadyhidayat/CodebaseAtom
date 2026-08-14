namespace BehaviorConsoleApp;

public interface ILogic<TInput, TOutput>
{
    public Task<TOutput> Handle(TInput input, CancellationToken cancellationToken = default);
}
