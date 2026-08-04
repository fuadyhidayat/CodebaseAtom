namespace Vioren.CodebaseExpress.Logics.WorkItems.DeleteWorkItem;

public sealed record DeleteWorkItemInput
{
    public required Guid WorkItemId { get; init; }
}
