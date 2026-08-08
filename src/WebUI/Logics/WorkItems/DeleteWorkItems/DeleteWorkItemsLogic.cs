namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.DeleteWorkItems;

public sealed class DeleteWorkItemsLogic(IDbContextFactory<DatabaseContext> databaseContextFactory)
{
    public async Task Handle(DeleteWorkItemsInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var workItems = await databaseContext.WorkItems
            .Where(workItem => input.WorkItemIds.Contains(workItem.Id))
            .ToListAsync(cancellationToken);

        databaseContext.WorkItems.RemoveRange(workItems);
        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
