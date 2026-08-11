namespace Vioren.CodebaseAtom.WebUI.Common.Exceptions;

public sealed class FormValidationException : Exception
{
    public IEnumerable<string> ErrorMessages { get; }

    public FormValidationException() : base()
    {
        ErrorMessages = new[] { "Form validation failed." };
    }

    public FormValidationException(string message) : base(message)
    {
        ErrorMessages = new[] { message };
    }

    public FormValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
        ErrorMessages = new[] { message };
    }

    public FormValidationException(string[] errorMessages)
        : base($"There {(errorMessages.Length > 1 ? "are" : "is")} {errorMessages.Length} validation failure{(errorMessages.Length > 1 ? "s" : "")}.")
    {
        ErrorMessages = errorMessages;
    }
}

