using Vioren.CodebaseExpress.Services.FileStorage;

namespace Vioren.CodebaseExpress.Logics.Projects.DeleteProject;

public sealed class DeleteProjectLogic(
    IDatabaseService databaseService,
    IFileStorageService fileStorageService)
    : ILogic<DeleteProjectInput, Unit>
{
    public async Task<Unit> Handle(DeleteProjectInput input, CancellationToken cancellationToken = default)
    {
        var project = await databaseService.Projects
            .Where(project => !project.IsDeleted && project.Id == input.ProjectId)
            .Include(project => project.WorkItems)
            .Include(project => project.Documents)
            .SingleOrDefaultAsync(cancellationToken)
           ?? throw new EntityNotFoundException(DomainDisplayTextFor.Project, DomainDisplayTextFor.Id, input.ProjectId);

        foreach (var document in project.Documents)
        {
            await fileStorageService.DeleteAsync(document.FilePath, cancellationToken);
        }

        databaseService.Documents.RemoveRange(project.Documents);
        databaseService.WorkItems.RemoveRange(project.WorkItems);
        _ = databaseService.Projects.Remove(project);
        _ = await databaseService.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
