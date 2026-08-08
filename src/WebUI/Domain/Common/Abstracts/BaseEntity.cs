namespace Vioren.CodebaseAtom.WebUI.Domain.Common.Abstracts;

public abstract class BaseEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
    public Guid CreatedBy { get; set; }
    public DateTimeOffset? Modified { get; set; }
    public Guid? ModifiedBy { get; set; }
}
