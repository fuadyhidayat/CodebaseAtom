using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Vioren.CodebaseExpress.Infrastructure.Database;

public sealed partial class DatabaseMigrator(ILogger<DatabaseMigrator> logger, DatabaseService databaseContext)
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Applying database migration...")]
    private static partial void LogApplyingMigration(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "Database is up to date. No database migration required.")]
    private static partial void LogDatabaseUpToDate(ILogger logger);

    public async Task Migrate()
    {
        var pendingMigrations = await databaseContext.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            LogApplyingMigration(logger);

            await databaseContext.Database.MigrateAsync();
        }
        else
        {
            LogDatabaseUpToDate(logger);
        }
    }
}
