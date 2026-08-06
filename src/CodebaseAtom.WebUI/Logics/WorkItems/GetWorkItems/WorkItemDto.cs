namespace CodebaseAtom.WebUI.Logics.WorkItems.GetWorkItems;

public sealed record WorkItemDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateOnly Deadline { get; init; }
    public required WorkItemStatus Status { get; init; }
}
