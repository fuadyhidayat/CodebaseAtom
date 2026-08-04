using Vioren.CodebaseExpress.Services.CurrentUser;

namespace Vioren.CodebaseExpress.Infrastructure.CurrentUser;

public static class ConfigureCurrentUser
{
    public static IServiceCollection AddCurrentUserService(this IServiceCollection services)
    {
        _ = services.AddTransient<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
