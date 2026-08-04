namespace CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItem;

public sealed class UpdateWorkItemLogic(DatabaseService databaseService)
{
    public async Task Handle(UpdateWorkItemInput input, CancellationToken cancellationToken = default)
    {
        var workItem = await databaseService.WorkItems
            .Where(workItem => workItem.Id == input.WorkItemId)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Id, input.WorkItemId);

        workItem.Title = input.Title;
        workItem.Description = input.Description;
        workItem.Deadline = input.Deadline;

        _ = await databaseService.SaveChangesAsync(cancellationToken);
    }
}
