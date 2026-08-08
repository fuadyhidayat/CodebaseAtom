namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.DeleteWorkItems;

public sealed record DeleteWorkItemsInput
{
    public required IEnumerable<Guid> WorkItemIds { get; init; }
}
