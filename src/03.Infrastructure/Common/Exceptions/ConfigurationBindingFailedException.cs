namespace Vioren.CodebaseExpress.Infrastructure.Common.Exceptions;

public sealed class ConfigurationBindingFailedException : Exception
{
    public ConfigurationBindingFailedException() : base() { }

    public ConfigurationBindingFailedException(string message) : base(message) { }

    public ConfigurationBindingFailedException(string message, Exception innerException) : base(message, innerException) { }

    public ConfigurationBindingFailedException(string configurationSection, Type targetType)
        : base($"Failed to bind configuration section '{configurationSection}' to instance of type {targetType.Name}.")
    {
    }
}
