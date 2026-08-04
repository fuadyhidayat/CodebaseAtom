namespace Vioren.CodebaseExpress.Domain.Entities;

public sealed class WorkItem : ModifiableEntity
{
    public required Guid ProjectId { get; init; }
    public Project Project { get; init; } = default!;

    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateOnly Deadline { get; set; }
    public required WorkItemStatus Status { get; set; }
}
