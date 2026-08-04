namespace CodebaseAtom.WebUI.Logics.Common.Exceptions;

public class ModelValidationException : Exception
{
    public ModelValidationException()
        : base("One or more validation failures have occurred. See ErrorMessages property for details.")
    {
        ValidationFailures = new Dictionary<string, string[]>();
    }

    public ModelValidationException(string message) : base(message)
    {
        ValidationFailures = new Dictionary<string, string[]>();
    }

    public ModelValidationException(string message, Exception innerException) : base(message, innerException)
    {
        ValidationFailures = new Dictionary<string, string[]>();
    }

    public ModelValidationException(IDictionary<string, string[]> validationFailures)
        : base("One or more validation failures have occurred. See ErrorMessages property for details.")
    {
        ValidationFailures = validationFailures;
    }

    public IDictionary<string, string[]> ValidationFailures { get; }

    public IReadOnlyCollection<string> ErrorMessages
    {
        get
        {
            var errorMessages = new List<string>();

            foreach (var validationFailure in ValidationFailures)
            {
                foreach (var errorMessage in validationFailure.Value)
                {
                    errorMessages.Add($"{validationFailure.Key}: {errorMessage}");
                }
            }

            return errorMessages;
        }
    }
}
