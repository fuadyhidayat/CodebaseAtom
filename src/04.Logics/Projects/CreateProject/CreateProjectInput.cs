namespace Vioren.CodebaseExpress.Logics.Projects.CreateProject;

public sealed record CreateProjectInput
{
    public required string Title { get; init; }
    public required string Description { get; init; }
}
