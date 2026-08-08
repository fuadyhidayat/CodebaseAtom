using Vioren.CodebaseAtom.WebUI.Infrastructure.Database.InitialData;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Database.Seeders;

public sealed class InitialDataSeeder(IDbContextFactory<DatabaseContext> databaseContextFactory)
{
    public async Task SeedInitialData()
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync();

        foreach (var project in InitialProjects.All)
        {
            if (!await databaseContext.Projects.AnyAsync(x => x.Id == project.Id))
            {
                _ = await databaseContext.Projects.AddAsync(project);
            }
        }

        foreach (var workItem in InitialWorkItems.All)
        {
            if (!await databaseContext.WorkItems.AnyAsync(x => x.Id == workItem.Id))
            {
                _ = await databaseContext.WorkItems.AddAsync(workItem);
            }
        }

        _ = await databaseContext.SaveChangesAsync();
    }
}
