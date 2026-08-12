namespace Vioren.CodebaseAtom.WebUI.Logics.Common.Exceptions;

public sealed class ModelValidationException : Exception
{
    public IEnumerable<string> ErrorMessages { get; }

    public ModelValidationException() : base()
    {
        ErrorMessages = new[] { "There is one or more validation failures." };
    }

    public ModelValidationException(string message) : base(message)
    {
        ErrorMessages = new[] { message };
    }

    public ModelValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
        ErrorMessages = new[] { message };
    }

    public ModelValidationException(string[] errorMessages)
        : base($"There {(errorMessages.Length > 1 ? "are" : "is")} {errorMessages.Length} validation failure{(errorMessages.Length > 1 ? "s" : "")}.")
    {
        ErrorMessages = errorMessages;
    }
}
