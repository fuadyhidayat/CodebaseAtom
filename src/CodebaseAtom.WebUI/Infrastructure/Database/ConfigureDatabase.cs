using Microsoft.EntityFrameworkCore.Diagnostics;
using CodebaseAtom.WebUI.Infrastructure.Common.Exceptions;
using CodebaseAtom.WebUI.Infrastructure.Database.Interceptors;
using CodebaseAtom.WebUI.Infrastructure.Database.Seeders;

namespace CodebaseAtom.WebUI.Infrastructure.Database;

public static class ConfigureDatabase
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseOptions = configuration.GetRequiredSection(DatabaseOptions.SectionKey).Get<DatabaseOptions>()
            ?? throw new ConfigurationBindingFailedException(DatabaseOptions.SectionKey, typeof(DatabaseOptions));

        _ = services.AddDbContext<IDatabaseService, DatabaseService>(options =>
        {
            _ = options.UseSqlServer(databaseOptions.ConnectionString, builder =>
            {
                _ = builder.MigrationsAssembly(typeof(DatabaseService).Assembly.FullName);
                _ = builder.MigrationsHistoryTable("__EFMigrationsHistory", DatabaseService.SchemaName);
                _ = builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            _ = options.ConfigureWarnings(wcb => wcb.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            _ = options.ConfigureWarnings(wcb => wcb.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        }, ServiceLifetime.Transient);

        _ = services.AddTransient<AuditingSaveChangesInterceptor>();
        _ = services.AddTransient<DatabaseMigrator>();
        _ = services.AddTransient<InitialDataSeeder>();

        return services;
    }

    public static async Task InitializeDatabase(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        var databaseMigrator = serviceProvider.GetRequiredService<DatabaseMigrator>();
        await databaseMigrator.Migrate();

        var initialDataSeeder = serviceProvider.GetRequiredService<InitialDataSeeder>();
        await initialDataSeeder.SeedInitialData();
    }
}
