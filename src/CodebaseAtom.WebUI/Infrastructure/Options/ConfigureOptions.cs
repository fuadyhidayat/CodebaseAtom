namespace CodebaseAtom.WebUI.Infrastructure.Options;

public static class ConfigureOptions
{
    public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.Configure<ApplicationOptions>(configuration.GetRequiredSection("Application"));

        return services;
    }
}
