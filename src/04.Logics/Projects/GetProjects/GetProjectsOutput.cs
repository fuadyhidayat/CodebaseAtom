namespace Vioren.CodebaseExpress.Logics.Projects.GetProjects;

public sealed record GetProjectsOutput
{
    public required IReadOnlyList<ProjectDto> Items { get; init; }
}
