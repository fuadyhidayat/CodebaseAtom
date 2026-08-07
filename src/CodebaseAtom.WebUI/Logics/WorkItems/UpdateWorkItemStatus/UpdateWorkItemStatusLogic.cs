namespace CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItemStatus;

public sealed class UpdateWorkItemStatusLogic(DatabaseContext databaseContext)
{
    public async Task Handle(UpdateWorkItemStatusInput input, CancellationToken cancellationToken = default)
    {
        var workItem = await databaseContext.WorkItems
            .Where(workItem => workItem.Id == input.WorkItemId)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Id, input.WorkItemId);

        workItem.Status = input.Status;
        workItem.Modified = DateTimeOffset.Now;
        workItem.ModifiedBy = input.ModifiedBy;

        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
