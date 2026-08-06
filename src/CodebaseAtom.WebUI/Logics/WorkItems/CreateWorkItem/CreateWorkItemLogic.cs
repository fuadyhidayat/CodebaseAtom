namespace CodebaseAtom.WebUI.Logics.WorkItems.CreateWorkItem;


public sealed class CreateWorkItemLogic(DatabaseContext databaseContext)
{
    public async Task<CreateWorkItemOutput> Handle(CreateWorkItemInput input, CancellationToken cancellationToken = default)
    {
        var workItem = new WorkItem
        {
            ProjectId = input.ProjectId,
            Title = input.Title,
            Description = input.Description,
            Deadline = input.Deadline,
            Status = WorkItemStatus.NotStarted,
            CreatedBy = input.CreatedBy
        };

        _ = await databaseContext.WorkItems.AddAsync(workItem, cancellationToken);
        _ = await databaseContext.SaveChangesAsync(cancellationToken);

        return new CreateWorkItemOutput { Id = workItem.Id };
    }
}
