using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Vioren.CodebaseExpress.Infrastructure.Database.Interceptors;
using Vioren.CodebaseExpress.Services.Database;

namespace Vioren.CodebaseExpress.Infrastructure.Database;

public class DatabaseService(
    DbContextOptions<DatabaseService> options,
    AuditingSaveChangesInterceptor auditingSaveChangesInterceptor)
    : DbContext(options), IDatabaseService
{
    public const string SchemaName = nameof(CodebaseExpress);

    public DbSet<Project> Projects { get; set; }
    public DbSet<WorkItem> WorkItems { get; set; }
    public DbSet<Document> Documents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _ = optionsBuilder.AddInterceptors(auditingSaveChangesInterceptor);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.HasDefaultSchema(SchemaName);
        _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
