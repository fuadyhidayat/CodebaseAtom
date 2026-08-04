namespace Vioren.CodebaseExpress.Logics.Statistics.GetStatistic;

public sealed class GetStatisticLogic(IDatabaseService databaseService)
    : ILogic<GetStatisticInput, GetStatisticOutput>
{
    public async Task<GetStatisticOutput> Handle(GetStatisticInput input, CancellationToken cancellationToken = default)
    {
        var projects = await databaseService.Projects
            .AsNoTracking()
            .Select(project => new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                DocumentsCount = project.Documents.Count,
                WorkItemsCount = project.WorkItems.Count
            })
            .ToListAsync(cancellationToken);

        return new GetStatisticOutput { Projects = projects };
    }
}
