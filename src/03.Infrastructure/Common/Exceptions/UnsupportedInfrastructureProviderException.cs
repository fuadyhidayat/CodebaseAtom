namespace Vioren.CodebaseExpress.Infrastructure.Common.Exceptions;

public sealed class UnsupportedInfrastructureProviderException : Exception
{
    public UnsupportedInfrastructureProviderException() : base() { }

    public UnsupportedInfrastructureProviderException(string message) : base(message) { }

    public UnsupportedInfrastructureProviderException(string message, Exception innerException) : base(message, innerException) { }

    public UnsupportedInfrastructureProviderException(string infrastructureName, string providerName)
        : base($"Unsupported {infrastructureName} provider: {providerName}")
    {
    }
}
