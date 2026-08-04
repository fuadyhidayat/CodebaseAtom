namespace Vioren.CodebaseExpress.Logics.WorkItems.UpdateWorkItemStatus;

public sealed class UpdateWorkItemStatusLogic(IDatabaseService databaseService)
    : ILogic<UpdateWorkItemStatusInput, Unit>
{
    public async Task<Unit> Handle(UpdateWorkItemStatusInput input, CancellationToken cancellationToken = default)
    {
        var workItem = await databaseService.WorkItems
            .FirstOrDefaultAsync(workItem => !workItem.IsDeleted && workItem.Id == input.WorkItemId, cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Id, input.WorkItemId);

        workItem.Status = input.NewStatus;

        _ = await databaseService.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
