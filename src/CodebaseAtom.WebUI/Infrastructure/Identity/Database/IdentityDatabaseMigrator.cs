namespace CodebaseAtom.WebUI.Infrastructure.Identity.Database;

public sealed class IdentityDatabaseMigrator(IdentityDatabaseContext identityDatabaseContext)
{
    public async Task Migrate()
    {
        await identityDatabaseContext.Database.MigrateAsync();
    }
}
