namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItem;

public sealed class UpdateWorkItemLogic(IDbContextFactory<DatabaseContext> databaseContextFactory)
{
    public async Task Handle(UpdateWorkItemInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var workItem = await databaseContext.WorkItems
            .Where(workItem => workItem.Id == input.WorkItemId)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Id, input.WorkItemId);

        workItem.Title = input.Title;
        workItem.Description = input.Description;
        workItem.Deadline = input.Deadline;
        workItem.Status = input.Status;

        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
