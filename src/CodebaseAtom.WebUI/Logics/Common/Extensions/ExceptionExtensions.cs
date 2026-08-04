namespace CodebaseAtom.WebUI.Logics.Common.Extensions;

public static class ExceptionExtensions
{
    public static IReadOnlyList<string> GetAllErrorMessages(this Exception exception)
    {
        var errorMessages = new List<string>();

        var current = exception;

        while (current is not null)
        {
            errorMessages.Add(current.Message);

            current = current.InnerException;
        }

        return errorMessages;
    }
}
