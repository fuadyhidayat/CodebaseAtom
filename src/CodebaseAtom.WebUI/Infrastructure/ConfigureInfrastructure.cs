using CodebaseAtom.WebUI.Infrastructure.FileStorage;
using CodebaseAtom.WebUI.Infrastructure.Identity;

namespace CodebaseAtom.WebUI.Infrastructure;

public static class ConfigureInfrastructure
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        _ = builder.Services.AddApplicationOptions(builder.Configuration);
        _ = builder.Services.AddDatabaseContext(builder.Configuration);
        _ = builder.Services.AddIdentityService(builder.Configuration);
        _ = builder.Services.AddFileStorage(builder.Configuration);

        return builder;
    }
}
