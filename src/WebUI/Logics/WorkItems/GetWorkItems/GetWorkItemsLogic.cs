namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.GetWorkItems;

public sealed class GetWorkItemsLogic(IDbContextFactory<DatabaseContext> databaseContextFactory)
{
    public async Task<GetWorkItemsOutput> Handle(GetWorkItemsInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var items = await databaseContext.WorkItems
            .AsNoTracking()
            .Where(workItem => workItem.ProjectId == input.ProjectId)
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
