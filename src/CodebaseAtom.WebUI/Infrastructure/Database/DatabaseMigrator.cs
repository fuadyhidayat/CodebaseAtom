namespace CodebaseAtom.WebUI.Infrastructure.Database;

public sealed class DatabaseMigrator(DatabaseService databaseContext)
{
    public async Task Migrate()
    {
        await databaseContext.Database.MigrateAsync();
    }
}
