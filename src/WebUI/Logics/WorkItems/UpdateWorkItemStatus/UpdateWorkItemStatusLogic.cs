namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItemStatus;

public sealed class UpdateWorkItemStatusLogic(IDbContextFactory<DatabaseContext> databaseContextFactory)
{
    public async Task Handle(UpdateWorkItemStatusInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var workItem = await databaseContext.WorkItems
            .Where(workItem => workItem.Id == input.WorkItemId)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Id, input.WorkItemId);

        workItem.Status = input.Status;

        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
