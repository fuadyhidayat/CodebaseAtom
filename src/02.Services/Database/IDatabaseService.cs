using Microsoft.EntityFrameworkCore;
using Vioren.CodebaseExpress.Domain.Entities;

namespace Vioren.CodebaseExpress.Services.Database;

public interface IDatabaseService
{
    public DbSet<Project> Projects { get; }
    public DbSet<WorkItem> WorkItems { get; }
    public DbSet<Document> Documents { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
