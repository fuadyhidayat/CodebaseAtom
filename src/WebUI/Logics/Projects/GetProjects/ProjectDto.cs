namespace Vioren.CodebaseAtom.WebUI.Logics.Projects.GetProjects;

public sealed record ProjectDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
}
