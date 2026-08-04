namespace CodebaseAtom.WebUI.Logics.WorkItems.GetWorkItems;

public sealed record GetWorkItemsOutput
{
    public required IReadOnlyList<WorkItemDto> Items { get; init; }
}
