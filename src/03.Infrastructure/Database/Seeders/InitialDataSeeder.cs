using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vioren.CodebaseExpress.Infrastructure.Database.InitialData;

namespace Vioren.CodebaseExpress.Infrastructure.Database.Seeders;

public sealed partial class InitialDataSeeder(DatabaseService databaseContext, ILogger<InitialDataSeeder> logger)
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Seeding data {entityType} {entityName}...")]
    private static partial void LogSeedingData(ILogger logger, string entityType, string entityName);

    public async Task SeedInitialData()
    {
        foreach (var project in InitialProjects.All)
        {
            if (!await databaseContext.Projects.AnyAsync(x => x.Id == project.Id))
            {
                LogSeedingData(logger, nameof(Project), project.Title);

                _ = await databaseContext.Projects.AddAsync(project);
            }
        }

        foreach (var workItem in InitialWorkItems.All)
        {
            if (!await databaseContext.WorkItems.AnyAsync(x => x.Id == workItem.Id))
            {
                LogSeedingData(logger, nameof(WorkItem), workItem.Title);

                _ = await databaseContext.WorkItems.AddAsync(workItem);
            }
        }

        _ = await databaseContext.SaveChangesAsync();
    }
}
