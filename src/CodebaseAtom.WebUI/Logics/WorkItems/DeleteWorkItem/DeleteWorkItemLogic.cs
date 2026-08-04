namespace CodebaseAtom.WebUI.Logics.WorkItems.DeleteWorkItem;

public sealed class DeleteWorkItemLogic(DatabaseService databaseService)
{
    public async Task Handle(DeleteWorkItemInput input, CancellationToken cancellationToken = default)
    {
        var workItem = await databaseService.WorkItems
            .Where(workItem => workItem.Id == input.WorkItemId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Id, input.WorkItemId);

        _ = databaseService.WorkItems.Remove(workItem);
        _ = await databaseService.SaveChangesAsync(cancellationToken);
    }
}
