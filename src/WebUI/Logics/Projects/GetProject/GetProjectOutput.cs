namespace Vioren.CodebaseAtom.WebUI.Logics.Projects.GetProject;

public sealed record GetProjectOutput
{
    public required ProjectDto Project { get; init; }
}
