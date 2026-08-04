namespace Vioren.CodebaseExpress.Logics.Projects.GetProject;

public sealed record ProjectDto
{
    public required Guid Id { get; init; }

    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
}
