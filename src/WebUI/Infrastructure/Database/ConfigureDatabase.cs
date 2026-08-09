using Microsoft.EntityFrameworkCore.Diagnostics;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Common.Exceptions;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Database.Interceptors;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Database.Seeders;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Database;

public static class ConfigureDatabase
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseOptionsSection = configuration.GetRequiredSection(DatabaseOptions.SectionKey);
        var databaseOptions = databaseOptionsSection.Get<DatabaseOptions>()
            ?? throw new ConfigurationBindingFailedException(DatabaseOptions.SectionKey, typeof(DatabaseOptions));

        _ = services.Configure<DatabaseOptions>(databaseOptionsSection);

        _ = services.AddDbContextFactory<DatabaseContext>(options =>
        {
            _ = options.UseSqlServer(databaseOptions.ConnectionString, builder =>
            {
                _ = builder.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName);
                _ = builder.MigrationsHistoryTable("__EFMigrationsHistory", DatabaseContext.SchemaName);
                _ = builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
            _ = options.ConfigureWarnings(wcb => wcb.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            _ = options.ConfigureWarnings(wcb => wcb.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        });


        _ = services.AddScoped<AuditingSaveChangesInterceptor>();
        _ = services.AddScoped<InitialDataSeeder>();

        return services;
    }

    public static async Task InitializeDatabase(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        var databaseContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<DatabaseContext>>();

        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync();
        await databaseContext.Database.MigrateAsync();

        var initialDataSeeder = serviceProvider.GetRequiredService<InitialDataSeeder>();
        await initialDataSeeder.SeedInitialData();
    }
}
