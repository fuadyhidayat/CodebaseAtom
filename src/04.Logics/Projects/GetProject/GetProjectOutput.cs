namespace Vioren.CodebaseExpress.Logics.Projects.GetProject;

public sealed record GetProjectOutput
{
    public required ProjectDto Item { get; init; }
}
