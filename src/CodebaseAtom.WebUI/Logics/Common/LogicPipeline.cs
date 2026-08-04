namespace CodebaseAtom.WebUI.Logics.Common;

/// <summary>
/// Chains registered <see cref="ILogicBehavior{TInput,TOutput}"/> instances around an inner
/// <see cref="ILogic{TInput,TOutput}"/>, forming a middleware pipeline.
/// </summary>
internal sealed class LogicPipeline<TInput, TOutput>(
    ILogic<TInput, TOutput> inner,
    IEnumerable<ILogicBehavior<TInput, TOutput>> behaviors) : ILogic<TInput, TOutput>
{
    private readonly IReadOnlyList<ILogicBehavior<TInput, TOutput>> _reversedBehaviors =
        behaviors.Reverse().ToList();

    public Task<TOutput> Handle(TInput input, CancellationToken cancellationToken = default)
    {
        Func<Task<TOutput>> pipeline = () => inner.Handle(input, cancellationToken);

        foreach (var behavior in _reversedBehaviors)
        {
            var next = pipeline;
            var current = behavior;
            pipeline = () => current.Handle(input, next, cancellationToken);
        }

        return pipeline();
    }
}
