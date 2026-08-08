namespace Vioren.CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;

public sealed class GetStatisticLogic(IDbContextFactory<DatabaseContext> databaseContextFactory)
{
    public async Task<GetStatisticOutput> Handle(GetStatisticInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var query = databaseContext.Projects
           .AsNoTracking()
           .Select(project => new ProjectDto
           {
               Id = project.Id,
               Title = project.Title,
               DocumentsCount = project.Documents.Count,
               WorkItemsCount = project.WorkItems.Count
           });

        if (input.MaxItems.HasValue)
        {
            query = query.Take(input.MaxItems.Value);
        }

        var projects = await query.ToListAsync(cancellationToken);

        return new GetStatisticOutput { Projects = projects };
    }
}
