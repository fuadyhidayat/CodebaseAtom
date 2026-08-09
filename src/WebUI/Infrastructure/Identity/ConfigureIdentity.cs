using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Common.Exceptions;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity.Database;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity.Database.Seeders;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

public static class ConfigureIdentity
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
    {
        AddIdentityDatabaseContext(services, configuration);

        _ = services.AddCascadingAuthenticationState();
        _ = services.AddScoped<ApplicationUserClaimsPrincipalFactory>();
        _ = services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();

        _ = services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
        })
        .AddIdentityCookies();

        _ = services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.User.RequireUniqueEmail = false;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            options.Lockout.MaxFailedAccessAttempts = 3;
        })
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<IdentityDatabaseContext>()
        .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

        return services;
    }

    private static void AddIdentityDatabaseContext(IServiceCollection services, IConfiguration configuration)
    {
        var databaseOptions = configuration.GetRequiredSection(DatabaseOptions.SectionKey).Get<DatabaseOptions>()
            ?? throw new ConfigurationBindingFailedException(DatabaseOptions.SectionKey, typeof(DatabaseOptions));

        _ = services.AddDbContextFactory<IdentityDatabaseContext>(options =>
        {
            _ = options.UseSqlServer(databaseOptions.ConnectionString, builder =>
            {
                _ = builder.MigrationsAssembly(typeof(IdentityDatabaseContext).Assembly.FullName);
                _ = builder.MigrationsHistoryTable("__EFMigrationsHistory", IdentityDatabaseContext.SchemaName);
                _ = builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
            _ = options.ConfigureWarnings(wcb => wcb.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            _ = options.ConfigureWarnings(wcb => wcb.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        });

        _ = services.AddScoped<RoleSeeder>();
        _ = services.AddScoped<UserSeeder>();
    }

    public static async Task InitializeIdentityDatabase(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        var identityDatabaseContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<IdentityDatabaseContext>>();

        await using var identityDatabaseContext = await identityDatabaseContextFactory.CreateDbContextAsync();
        await identityDatabaseContext.Database.MigrateAsync();

        var roleSeeder = serviceProvider.GetRequiredService<RoleSeeder>();
        await roleSeeder.SeedRoles();

        var userSeeder = serviceProvider.GetRequiredService<UserSeeder>();
        await userSeeder.SeedUsers();
    }
}
