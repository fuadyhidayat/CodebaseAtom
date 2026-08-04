namespace CodebaseAtom.WebUI.Logics.WorkItems.CreateWorkItem;


public sealed class CreateWorkItemLogic(DatabaseService databaseService)
{
    public async Task<CreateWorkItemOutput> Handle(CreateWorkItemInput input, CancellationToken cancellationToken = default)
    {
        var workItem = new WorkItem
        {
            ProjectId = input.ProjectId,
            Title = input.Title,
            Description = input.Description,
            Deadline = input.Deadline,
            Status = WorkItemStatus.NotStarted
        };

        _ = await databaseService.WorkItems.AddAsync(workItem, cancellationToken);
        _ = await databaseService.SaveChangesAsync(cancellationToken);

        return new CreateWorkItemOutput { Id = workItem.Id };
    }
}
