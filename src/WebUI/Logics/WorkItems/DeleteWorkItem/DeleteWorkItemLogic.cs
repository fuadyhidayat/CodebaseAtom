namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.DeleteWorkItem;

public sealed class DeleteWorkItemLogic(IDbContextFactory<DatabaseContext> databaseContextFactory)
{
    public async Task Handle(DeleteWorkItemInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var workItem = await databaseContext.WorkItems
            .Where(workItem => workItem.Id == input.WorkItemId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Id, input.WorkItemId);

        _ = databaseContext.WorkItems.Remove(workItem);
        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
