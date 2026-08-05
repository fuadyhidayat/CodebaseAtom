namespace CodebaseAtom.WebUI.Logics.Common.Exceptions;

public sealed class EntityAlreadyExistsException : Exception
{
    public EntityAlreadyExistsException() : base() { }

    public EntityAlreadyExistsException(string message) : base(message) { }

    public EntityAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }

    public EntityAlreadyExistsException(string entityType, string fieldName, object fieldValue)
        : base($"{entityType} with {fieldName} '{fieldValue}' already exists.")
    {
    }
}
