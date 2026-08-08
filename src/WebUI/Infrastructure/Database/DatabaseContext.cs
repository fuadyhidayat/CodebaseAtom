using System.Reflection;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options)
    : DbContext(options)
{
    public const string SchemaName = nameof(CodebaseAtom);

    public DbSet<Project> Projects { get; set; }
    public DbSet<WorkItem> WorkItems { get; set; }
    public DbSet<Document> Documents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.HasDefaultSchema(SchemaName);
        _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
