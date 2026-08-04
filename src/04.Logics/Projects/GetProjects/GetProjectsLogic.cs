namespace Vioren.CodebaseExpress.Logics.Projects.GetProjects;

public sealed class GetProjectsLogic(IDatabaseService databaseService)
    : ILogic<GetProjectsInput, GetProjectsOutput>
{
    public async Task<GetProjectsOutput> Handle(GetProjectsInput input, CancellationToken cancellationToken = default)
    {
        var query = databaseService.Projects
            .AsNoTracking()
            .OrderBy(project => project.Title)
            .Select(project => new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description
            });

        if (input.MaxItems.HasValue)
        {
            query = query.Take(input.MaxItems.Value);
        }

        var items = await query.ToListAsync(cancellationToken);

        return new GetProjectsOutput { Items = items };
    }
}
