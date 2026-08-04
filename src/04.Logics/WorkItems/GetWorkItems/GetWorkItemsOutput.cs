namespace Vioren.CodebaseExpress.Logics.WorkItems.GetWorkItems;

public sealed record GetWorkItemsOutput
{
    public required IReadOnlyList<WorkItemDto> Items { get; init; }
}
