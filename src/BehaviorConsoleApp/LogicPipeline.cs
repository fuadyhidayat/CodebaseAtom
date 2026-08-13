namespace BehaviorConsoleApp;

public sealed class LogicPipeline<TInput, TOutput>(
    ILogic<TInput, TOutput> inner,
    IEnumerable<ILogicBehavior<TInput, TOutput>> behaviors) : ILogic<TInput, TOutput>
{
    public Task<TOutput> Handle(TInput input, CancellationToken cancellationToken = default)
    {
        var pipeline = () => inner.Handle(input, cancellationToken);

        foreach (var behavior in behaviors.Reverse())
        {
            var next = pipeline;
            var current = behavior;
            pipeline = () => current.Handle(input, next, cancellationToken);
        }

        return pipeline();
    }
}
