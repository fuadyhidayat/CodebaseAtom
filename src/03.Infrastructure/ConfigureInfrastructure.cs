using Microsoft.AspNetCore.Builder;
using Vioren.CodebaseExpress.Infrastructure.CurrentUser;
using Vioren.CodebaseExpress.Infrastructure.Database;
using Vioren.CodebaseExpress.Infrastructure.FileStorage;
using Vioren.CodebaseExpress.Infrastructure.Identity;
using Vioren.CodebaseExpress.Infrastructure.Logging;
using Vioren.CodebaseExpress.Infrastructure.Options;

namespace Vioren.CodebaseExpress.Infrastructure;

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
