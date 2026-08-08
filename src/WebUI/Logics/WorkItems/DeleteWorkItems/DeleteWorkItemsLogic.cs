namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.DeleteWorkItems;

public sealed class DeleteWorkItemsLogic(DatabaseContext databaseContext)
{
    public async Task Handle(DeleteWorkItemsInput input, CancellationToken cancellationToken = default)
    {
        var workItems = await databaseContext.WorkItems
            .Where(workItem => input.WorkItemIds.Contains(workItem.Id))
            .ToListAsync(cancellationToken);

        databaseContext.WorkItems.RemoveRange(workItems);
        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
