using Microsoft.EntityFrameworkCore.Diagnostics;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Common.Exceptions;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Database.Seeders;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Database;

public static class ConfigureDatabase
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseOptions = configuration.GetRequiredSection(DatabaseOptions.SectionKey).Get<DatabaseOptions>()
            ?? throw new ConfigurationBindingFailedException(DatabaseOptions.SectionKey, typeof(DatabaseOptions));

        _ = services.AddDbContext<DatabaseContext>(options =>
        {
            _ = options.UseSqlServer(databaseOptions.ConnectionString, builder =>
            {
                _ = builder.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName);
                _ = builder.MigrationsHistoryTable("__EFMigrationsHistory", DatabaseContext.SchemaName);
                _ = builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            _ = options.ConfigureWarnings(wcb => wcb.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            _ = options.ConfigureWarnings(wcb => wcb.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        }, ServiceLifetime.Transient);

        _ = services.AddTransient<InitialDataSeeder>();

        return services;
    }

    public static async Task InitializeDatabase(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        var databaseContext = serviceProvider.GetRequiredService<DatabaseContext>();
        await databaseContext.Database.MigrateAsync();

        var initialDataSeeder = serviceProvider.GetRequiredService<InitialDataSeeder>();
        await initialDataSeeder.SeedInitialData();
    }
}
