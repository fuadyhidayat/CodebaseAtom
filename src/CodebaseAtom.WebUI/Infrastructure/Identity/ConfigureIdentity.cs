using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using CodebaseAtom.WebUI.Infrastructure.Common.Exceptions;
using CodebaseAtom.WebUI.Infrastructure.Identity.Database;
using CodebaseAtom.WebUI.Infrastructure.Identity.Database.Seeders;
using CodebaseAtom.WebUI.Infrastructure.Identity.EmailSender;

namespace CodebaseAtom.WebUI.Infrastructure.Identity;

public static class ConfigureIdentity
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
    {
        AddIdentityDatabaseContext(services, configuration);

        _ = services.AddCascadingAuthenticationState();

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
        .AddSignInManager()
        .AddDefaultTokenProviders();

        _ = services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        return services;
    }

    private static void AddIdentityDatabaseContext(IServiceCollection services, IConfiguration configuration)
    {
        var identityOptionsSection = configuration.GetRequiredSection(IdentityOptions.SectionKey);
        var identityOptions = identityOptionsSection.Get<IdentityOptions>()
            ?? throw new ConfigurationBindingFailedException(IdentityOptions.SectionKey, typeof(IdentityOptions));

        _ = services.Configure<IdentityOptions>(identityOptionsSection);

        _ = services.AddDbContext<IdentityDatabaseContext>(options =>
        {
            _ = options.UseSqlServer(identityOptions.ConnectionString, builder =>
            {
                _ = builder.MigrationsAssembly(typeof(IdentityDatabaseContext).Assembly.FullName);
                _ = builder.MigrationsHistoryTable("__EFMigrationsHistory", IdentityDatabaseContext.SchemaName);
                _ = builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            _ = options.ConfigureWarnings(wcb => wcb.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            _ = options.ConfigureWarnings(wcb => wcb.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        }, ServiceLifetime.Transient);

        _ = services.AddScoped<IdentityDatabaseMigrator>();
        _ = services.AddTransient<RoleSeeder>();
        _ = services.AddTransient<UserSeeder>();
    }

    public static async Task InitializeIdentityDatabase(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        var identityDatabaseMigrator = serviceProvider.GetRequiredService<IdentityDatabaseMigrator>();
        await identityDatabaseMigrator.Migrate();

        var roleSeeder = serviceProvider.GetRequiredService<RoleSeeder>();
        await roleSeeder.SeedRoles();

        var userSeeder = serviceProvider.GetRequiredService<UserSeeder>();
        await userSeeder.SeedUsers();
    }
}
