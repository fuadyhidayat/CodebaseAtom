namespace CodebaseAtom.WebUI.Logics.Projects.UpdateProject;

public sealed class UpdateProjectLogic(DatabaseService databaseService)
{
    public async Task Handle(UpdateProjectInput input, CancellationToken cancellationToken = default)
    {
        var project = await databaseService.Projects
            .Where(project => project.Id == input.ProjectId)
            .SingleOrDefaultAsync(cancellationToken)
           ?? throw new EntityNotFoundException(nameof(Project), nameof(input.ProjectId), input.ProjectId);

        var anyProjectWithTheSameTitle = await databaseService.Projects
            .Where(project => project.Id != input.ProjectId && project.Title == input.Title)
            .AnyAsync(cancellationToken);

        if (anyProjectWithTheSameTitle)
        {
            throw new EntityAlreadyExistsException(nameof(Project), nameof(Project.Title), input.Title);
        }

        project.Title = input.Title;
        project.Description = input.Description;

        _ = await databaseService.SaveChangesAsync(cancellationToken);
    }
}
