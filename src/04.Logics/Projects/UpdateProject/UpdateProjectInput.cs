namespace Vioren.CodebaseExpress.Logics.Projects.UpdateProject;

public sealed record UpdateProjectInput
{
    public required Guid ProjectId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
}
