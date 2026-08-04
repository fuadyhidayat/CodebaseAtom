namespace CodebaseAtom.WebUI.Infrastructure.CurrentUser;

public static class ConfigureCurrentUser
{
    public static IServiceCollection AddCurrentUserService(this IServiceCollection services)
    {
        _ = services.AddTransient<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
