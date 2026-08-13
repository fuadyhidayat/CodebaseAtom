using Microsoft.Extensions.DependencyInjection;

namespace BehaviorConsoleApp;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogicWithPipeline<TLogic, TInput, TOutput>(this IServiceCollection services)
        where TLogic : class, ILogic<TInput, TOutput>
    {
        _ = services.AddScoped<TLogic>();
        _ = services.AddScoped<ILogic<TInput, TOutput>>(serviceProvider =>
            new LogicPipeline<TInput, TOutput>(
                serviceProvider.GetRequiredService<TLogic>(),
                serviceProvider.GetServices<ILogicBehavior<TInput, TOutput>>()
            ));
        return services;
    }
}
