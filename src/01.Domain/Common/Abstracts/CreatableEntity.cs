namespace Vioren.CodebaseExpress.Domain.Common.Abstracts;

public abstract class CreatableEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public bool IsDeleted { get; set; }
    public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
    public Guid CreatedBy { get; set; } = Guid.Empty;
}
