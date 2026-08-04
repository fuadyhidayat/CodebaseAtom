namespace CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItemStatus;

public sealed class UpdateWorkItemStatusLogic(IDatabaseService databaseService)
{
    public async Task Handle(UpdateWorkItemStatusInput input, CancellationToken cancellationToken = default)
    {
        var workItem = await databaseService.WorkItems
            .Where(workItem => workItem.Id == input.WorkItemId)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Id, input.WorkItemId);

        workItem.Status = input.NewStatus;

        _ = await databaseService.SaveChangesAsync(cancellationToken);
    }
}
