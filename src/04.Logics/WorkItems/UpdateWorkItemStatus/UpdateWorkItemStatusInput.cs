namespace Vioren.CodebaseExpress.Logics.WorkItems.UpdateWorkItemStatus;

public sealed record UpdateWorkItemStatusInput
{
    public required Guid WorkItemId { get; init; }
    public required WorkItemStatus NewStatus { get; init; }
}
