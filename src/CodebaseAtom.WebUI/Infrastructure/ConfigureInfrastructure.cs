using CodebaseAtom.WebUI.Infrastructure.CurrentUser;
using CodebaseAtom.WebUI.Infrastructure.FileStorage;
using CodebaseAtom.WebUI.Infrastructure.Identity;
using CodebaseAtom.WebUI.Infrastructure.Logging;

namespace CodebaseAtom.WebUI.Infrastructure;

public static class ConfigureInfrastructure
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.AddSerilogLogging();
        _ = builder.Services.AddApplicationOptions(builder.Configuration);
        _ = builder.Services.AddCurrentUserService();
        _ = builder.Services.AddDatabaseContext(builder.Configuration);
        _ = builder.Services.AddIdentityService(builder.Configuration);
        _ = builder.Services.AddFileStorage(builder.Configuration);

        return builder;
    }
}
