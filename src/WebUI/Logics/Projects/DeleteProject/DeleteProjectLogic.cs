using Vioren.CodebaseAtom.WebUI.Infrastructure.FileStorage;

namespace Vioren.CodebaseAtom.WebUI.Logics.Projects.DeleteProject;

public sealed class DeleteProjectLogic(
    IDbContextFactory<DatabaseContext> databaseContextFactory,
    FileStorageService fileStorageService)
{
    public async Task Handle(DeleteProjectInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var project = await databaseContext.Projects
            .Where(project => project.Id == input.ProjectId)
            .Include(project => project.WorkItems)
            .Include(project => project.Documents)
            .SingleOrDefaultAsync(cancellationToken)
           ?? throw new EntityNotFoundException(DomainDisplayTextFor.Project, DomainDisplayTextFor.Id, input.ProjectId);

        foreach (var document in project.Documents)
        {
            fileStorageService.Delete(document.FilePath);
        }

        databaseContext.Documents.RemoveRange(project.Documents);
        databaseContext.WorkItems.RemoveRange(project.WorkItems);
        _ = databaseContext.Projects.Remove(project);
        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
