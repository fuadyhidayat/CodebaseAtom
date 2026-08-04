namespace Vioren.CodebaseExpress.Logics.WorkItems.DeleteWorkItem;

public sealed class DeleteWorkItemLogic(IDatabaseService databaseService)
    : ILogic<DeleteWorkItemInput, Unit>
{
    public async Task<Unit> Handle(DeleteWorkItemInput input, CancellationToken cancellationToken = default)
    {
        var workItem = await databaseService.WorkItems
            .Where(workItem => workItem.Id == input.WorkItemId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Id, input.WorkItemId);

        _ = databaseService.WorkItems.Remove(workItem);
        _ = await databaseService.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
