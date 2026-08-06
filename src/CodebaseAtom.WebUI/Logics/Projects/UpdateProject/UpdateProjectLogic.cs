namespace CodebaseAtom.WebUI.Logics.Projects.UpdateProject;

public sealed class UpdateProjectLogic(DatabaseContext databaseContext)
{
    public async Task Handle(UpdateProjectInput input, CancellationToken cancellationToken = default)
    {
        var project = await databaseContext.Projects
            .Where(project => project.Id == input.ProjectId)
            .SingleOrDefaultAsync(cancellationToken)
           ?? throw new EntityNotFoundException(nameof(Project), nameof(input.ProjectId), input.ProjectId);

        var anyProjectWithTheSameTitle = await databaseContext.Projects
            .Where(project => project.Id != input.ProjectId && project.Title == input.Title)
            .AnyAsync(cancellationToken);

        if (anyProjectWithTheSameTitle)
        {
            throw new EntityAlreadyExistsException(nameof(Project), nameof(Project.Title), input.Title);
        }

        project.Title = input.Title;
        project.Description = input.Description;
        project.Modified = DateTimeOffset.Now;
        project.ModifiedBy = input.ModifiedBy;

        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
