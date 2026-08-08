namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItem;

public sealed record UpdateWorkItemInput
{
    public required Guid WorkItemId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateOnly Deadline { get; init; }
    public required WorkItemStatus Status { get; init; }
}
