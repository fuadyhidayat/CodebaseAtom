namespace CodebaseAtom.WebUI.Logics.Projects.CreateProject;

public sealed class CreateProjectLogic(DatabaseService databaseService)
{
    public async Task<CreateProjectOutput> Handle(CreateProjectInput input, CancellationToken cancellationToken = default)
    {
        var anyProjectWithTheSameTitle = await databaseService.Projects
            .Where(project => project.Title == input.Title)
            .AnyAsync(cancellationToken);

        if (anyProjectWithTheSameTitle)
        {
            throw new EntityAlreadyExistsException(nameof(Project), nameof(Project.Title), input.Title);
        }

        var project = new Project
        {
            Title = input.Title,
            Description = input.Description,
            CreatedBy = input.CreatedBy
        };

        _ = await databaseService.Projects.AddAsync(project, cancellationToken);
        _ = await databaseService.SaveChangesAsync(cancellationToken);

        return new CreateProjectOutput { Id = project.Id };
    }
}
