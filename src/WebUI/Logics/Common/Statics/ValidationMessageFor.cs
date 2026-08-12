namespace Vioren.CodebaseAtom.WebUI.Logics.Common.Statics;

public static class ValidationMessageFor
{
    public static string Required(string entityName, string fieldName)
    {
        return $"{entityName} {fieldName} is required.";
    }

    public static string MinimumLength(string entityName, string fieldName, int minimumLength)
    {
        return $"The minimum length for {entityName} {fieldName} is {minimumLength} characters.";
    }

    public static string MaximumLength(string entityName, string fieldName, int maximumLength)
    {
        return $"The maximum length for {entityName} {fieldName} is {maximumLength} characters.";
    }
}
