namespace CodebaseAtom.WebUI.Domain.Common.Abstracts;

public abstract class BaseEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public DateTimeOffset Created { get; init; } = DateTimeOffset.Now;
    public required Guid CreatedBy { get; init; }
    public DateTimeOffset? Modified { get; set; }
    public Guid? ModifiedBy { get; set; }
}
