namespace CodebaseAtom.WebUI.Domain.Common.Abstracts;

public abstract class CreatableEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public DateTimeOffset Created { get; init; } = DateTimeOffset.Now;
    public required Guid CreatedBy { get; init; }
}
