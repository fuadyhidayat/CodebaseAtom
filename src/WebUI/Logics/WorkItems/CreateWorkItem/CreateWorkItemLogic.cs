namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.CreateWorkItem;


public sealed class CreateWorkItemLogic(IDbContextFactory<DatabaseContext> databaseContextFactory)
{
    public async Task<CreateWorkItemOutput> Handle(CreateWorkItemInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var workItem = new WorkItem
        {
            ProjectId = input.ProjectId,
            Title = input.Title,
            Description = input.Description,
            Deadline = input.Deadline,
            Status = WorkItemStatus.NotStarted
        };

        _ = await databaseContext.WorkItems.AddAsync(workItem, cancellationToken);
        _ = await databaseContext.SaveChangesAsync(cancellationToken);

        return new CreateWorkItemOutput { WorkItemId = workItem.Id };
    }
}
