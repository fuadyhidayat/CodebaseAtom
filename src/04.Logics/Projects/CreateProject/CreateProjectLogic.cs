namespace Vioren.CodebaseExpress.Logics.Projects.CreateProject;

public sealed class CreateProjectLogic(IDatabaseService databaseService)
    : ILogic<CreateProjectInput, CreateProjectOutput>
{
    public async Task<CreateProjectOutput> Handle(CreateProjectInput input, CancellationToken cancellationToken = default)
    {
        var anyProjectWithTheSameTitle = await databaseService.Projects
            .Where(project => !project.IsDeleted && project.Title == input.Title)
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

        _ = await databaseService.Projects.AddAsync(project, cancellationToken);
        _ = await databaseService.SaveChangesAsync(cancellationToken);

        return new CreateProjectOutput { Id = project.Id };
    }
}
