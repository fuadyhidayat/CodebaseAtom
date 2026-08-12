namespace Vioren.CodebaseAtom.WebUI.Logics.Projects.UpdateProject;

public sealed class UpdateProjectLogic(
    IDbContextFactory<DatabaseContext> databaseContextFactory,
    IValidator<UpdateProjectInput> validator)
{
    public async Task Handle(UpdateProjectInput input, CancellationToken cancellationToken = default)
    {
        await validator.ValidateInputAsync(input, cancellationToken);

        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

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

        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
