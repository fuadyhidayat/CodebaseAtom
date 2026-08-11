namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.CreateWorkItem;

public sealed record CreateWorkItemOutput
{
    public required Guid WorkItemId { get; init; }
}
