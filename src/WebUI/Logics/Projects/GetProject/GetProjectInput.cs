namespace Vioren.CodebaseAtom.WebUI.Logics.Projects.GetProject;

public sealed record GetProjectInput
{
    public required Guid ProjectId { get; init; }
}
