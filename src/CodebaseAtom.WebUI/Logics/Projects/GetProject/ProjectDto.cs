namespace CodebaseAtom.WebUI.Logics.Projects.GetProject;

public sealed record ProjectDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required Guid CreatedBy { get; init; }
    public string CreatedByUsername { get; set; } = string.Empty;
    public string CreatedByEmail { get; set; } = string.Empty;
    public string CreatedByDisplayName { get; set; } = string.Empty;
}
