namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.DeleteWorkItem;

public sealed class DeleteWorkItemLogic(DatabaseContext databaseContext)
{
    public async Task Handle(DeleteWorkItemInput input, CancellationToken cancellationToken = default)
    {
        var workItem = await databaseContext.WorkItems
            .Where(workItem => workItem.Id == input.WorkItemId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Id, input.WorkItemId);

        _ = databaseContext.WorkItems.Remove(workItem);
        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
