namespace Vioren.CodebaseAtom.WebUI.Infrastructure.CurrentUser;

public static class ConfigureCurrentUser
{
    public static IServiceCollection AddCurrentUserService(this IServiceCollection services)
    {
        _ = services.AddScoped<CurrentUserService>();
        //_ = services.AddScoped<CurrentUserState>();

        return services;
    }
}
