namespace CodebaseAtom.WebUI.Domain.Common.Abstracts;

public abstract class ModifiableEntity : CreatableEntity
{
    public DateTimeOffset? Modified { get; set; }
    public Guid? ModifiedBy { get; set; }
}
