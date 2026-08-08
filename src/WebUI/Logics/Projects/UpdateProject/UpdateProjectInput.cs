namespace Vioren.CodebaseAtom.WebUI.Logics.Projects.UpdateProject;

public sealed record UpdateProjectInput
{
    public required Guid ProjectId { get; init; }
    public required Guid ModifiedBy { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
}
