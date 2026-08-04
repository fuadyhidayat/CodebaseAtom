namespace Vioren.CodebaseExpress.Logics.Projects.GetProject;

public sealed record GetProjectInput
{
    public required Guid Id { get; init; }
}
