namespace Vioren.CodebaseExpress.Logics.WorkItems.UpdateWorkItem;

public sealed class UpdateWorkItemLogic(IDatabaseService databaseService)
    : ILogic<UpdateWorkItemInput, Unit>
{
    public async Task<Unit> Handle(UpdateWorkItemInput input, CancellationToken cancellationToken = default)
    {
        var workItem = await databaseService.WorkItems
            .FirstOrDefaultAsync(workItem => !workItem.IsDeleted && workItem.Id == input.WorkItemId, cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Id, input.WorkItemId);

        workItem.Title = input.Title;
        workItem.Description = input.Description;
        workItem.Deadline = input.Deadline;

        _ = await databaseService.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
