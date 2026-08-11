namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.GetWorkItems;

public sealed record GetWorkItemsOutput
{
    public required IReadOnlyList<WorkItemDto> WorkItems { get; init; }
}
