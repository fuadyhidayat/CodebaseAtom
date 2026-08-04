namespace Vioren.CodebaseExpress.Logics.WorkItems.GetWorkItems;

public sealed record GetWorkItemsInput
{
    public required Guid ProjectId { get; init; }
}
