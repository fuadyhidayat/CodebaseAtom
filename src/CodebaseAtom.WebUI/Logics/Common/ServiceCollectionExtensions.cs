namespace CodebaseAtom.WebUI.Logics.Common;

internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers <typeparamref name="TLogic"/> and an <see cref="ILogic{TInput,TOutput}"/> pipeline
    /// that wraps it with all registered <see cref="ILogicBehavior{TInput,TOutput}"/> instances.
    /// </summary>
    internal static IServiceCollection AddLogicWithPipeline<TLogic, TInput, TOutput>(
        this IServiceCollection services)
        where TLogic : class, ILogic<TInput, TOutput>
    {
        _ = services.AddTransient<TLogic>();
        _ = services.AddTransient<ILogic<TInput, TOutput>>(sp =>
            new LogicPipeline<TInput, TOutput>(
                sp.GetRequiredService<TLogic>(),
                sp.GetServices<ILogicBehavior<TInput, TOutput>>()
            ));
        return services;
    }
}
