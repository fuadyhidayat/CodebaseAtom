namespace CodebaseAtom.WebUI.Domain.Entities;

public sealed class Project : ModifiableEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }

    public ICollection<WorkItem> WorkItems { get; private set; } = new List<WorkItem>();
    public ICollection<Document> Documents { get; private set; } = new List<Document>();
}
