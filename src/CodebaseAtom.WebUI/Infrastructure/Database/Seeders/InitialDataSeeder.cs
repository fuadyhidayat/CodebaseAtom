using CodebaseAtom.WebUI.Infrastructure.Database.InitialData;

namespace CodebaseAtom.WebUI.Infrastructure.Database.Seeders;

public sealed class InitialDataSeeder(DatabaseService databaseContext)
{
    public async Task SeedInitialData()
    {
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
