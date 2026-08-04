namespace Vioren.CodebaseExpress.Logics.Projects.DeleteProject;

public sealed record DeleteProjectInput
{
    public required Guid ProjectId { get; init; }
}
