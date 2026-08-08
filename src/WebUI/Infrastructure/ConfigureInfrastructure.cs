using Vioren.CodebaseAtom.WebUI.Infrastructure.CurrentUser;
using Vioren.CodebaseAtom.WebUI.Infrastructure.FileStorage;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure;

public static class ConfigureInfrastructure
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        _ = builder.Services.AddCurrentUserService();
        _ = builder.Services.AddApplicationOptions(builder.Configuration);
        _ = builder.Services.AddDatabaseContext(builder.Configuration);
        _ = builder.Services.AddIdentityService(builder.Configuration);
        _ = builder.Services.AddFileStorage(builder.Configuration);

        return builder;
    }
}
