namespace Vioren.CodebaseAtom.WebUI.Logics.Projects.GetProjects;

public sealed class GetProjectsLogic(IDbContextFactory<DatabaseContext> databaseContextFactory)
{
    public async Task<GetProjectsOutput> Handle(CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var query = databaseContext.Projects
            .AsNoTracking()
            .OrderBy(project => project.Title)
            .Select(project => new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description
            });

        var projects = await query.ToListAsync(cancellationToken);

        return new GetProjectsOutput { Projects = projects };
    }
}
