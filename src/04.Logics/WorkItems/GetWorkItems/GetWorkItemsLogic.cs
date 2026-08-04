namespace Vioren.CodebaseExpress.Logics.WorkItems.GetWorkItems;

public sealed class GetWorkItemsLogic(IDatabaseService databaseService)
    : ILogic<GetWorkItemsInput, GetWorkItemsOutput>
{
    public async Task<GetWorkItemsOutput> Handle(GetWorkItemsInput input, CancellationToken cancellationToken = default)
    {
        var items = await databaseService.WorkItems
            .AsNoTracking()
            .Where(workItem => !workItem.IsDeleted && workItem.ProjectId == input.ProjectId)
            .Select(workItem => new WorkItemDto
            {
                Id = workItem.Id,
                Title = workItem.Title,
                Description = workItem.Description,
                Deadline = workItem.Deadline,
                Status = workItem.Status
            })
            .ToListAsync(cancellationToken);

        return new GetWorkItemsOutput { Items = items };
    }
}
