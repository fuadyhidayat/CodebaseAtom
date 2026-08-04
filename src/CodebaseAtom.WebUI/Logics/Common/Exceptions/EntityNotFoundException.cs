namespace CodebaseAtom.WebUI.Logics.Common.Exceptions;

public sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException() : base() { }

    public EntityNotFoundException(string message) : base(message) { }

    public EntityNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    public EntityNotFoundException(string entityType, string fieldName, object fieldValue)
        : base($"{entityType} with {fieldName} '{fieldValue}' could not be found.")
    {
    }
}
