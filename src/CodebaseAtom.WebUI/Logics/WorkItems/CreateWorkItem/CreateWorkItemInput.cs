namespace CodebaseAtom.WebUI.Logics.WorkItems.CreateWorkItem;

public sealed record CreateWorkItemInput
{
    public required Guid ProjectId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateOnly Deadline { get; init; }
}
