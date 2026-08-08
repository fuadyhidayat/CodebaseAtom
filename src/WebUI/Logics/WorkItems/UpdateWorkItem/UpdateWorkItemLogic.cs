namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItem;

public sealed class UpdateWorkItemLogic(DatabaseContext databaseContext)
{
    public async Task Handle(UpdateWorkItemInput input, CancellationToken cancellationToken = default)
    {
        var workItem = await databaseContext.WorkItems
            .Where(workItem => workItem.Id == input.WorkItemId)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Id, input.WorkItemId);

        workItem.Title = input.Title;
        workItem.Description = input.Description;
        workItem.Deadline = input.Deadline;
        workItem.Status = input.Status;
        workItem.Modified = DateTimeOffset.Now;
        workItem.ModifiedBy = input.ModifiedBy;

        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
