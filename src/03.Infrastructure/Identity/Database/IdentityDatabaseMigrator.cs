using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Vioren.CodebaseExpress.Infrastructure.Identity.Database;

public sealed partial class IdentityDatabaseMigrator(ILogger<IdentityDatabaseMigrator> logger, IdentityDatabaseContext identityDatabaseContext)
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Applying identity database migration...")]
    private static partial void LogApplyingMigration(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "Identity database is up to date. No database migration required.")]
    private static partial void LogDatabaseUpToDate(ILogger logger);

    public async Task Migrate()
    {
        var pendingMigrations = await identityDatabaseContext.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            LogApplyingMigration(logger);

            await identityDatabaseContext.Database.MigrateAsync();
        }
        else
        {
            LogDatabaseUpToDate(logger);
        }
    }
}
