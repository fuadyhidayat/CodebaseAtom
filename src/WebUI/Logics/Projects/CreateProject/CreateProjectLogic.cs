namespace Vioren.CodebaseAtom.WebUI.Logics.Projects.CreateProject;

public sealed class CreateProjectLogic(IDbContextFactory<DatabaseContext> databaseContextFactory)
{
    public async Task<CreateProjectOutput> Handle(CreateProjectInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var anyProjectWithTheSameTitle = await databaseContext.Projects
            .Where(project => project.Title == input.Title)
            .AnyAsync(cancellationToken);

        if (anyProjectWithTheSameTitle)
        {
            throw new EntityAlreadyExistsException(nameof(Project), nameof(Project.Title), input.Title);
        }

        var project = new Project
        {
            Title = input.Title,
            Description = input.Description
        };

        _ = await databaseContext.Projects.AddAsync(project, cancellationToken);
        _ = await databaseContext.SaveChangesAsync(cancellationToken);

        return new CreateProjectOutput { ProjectId = project.Id };
    }
}
