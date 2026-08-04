namespace Vioren.CodebaseExpress.Services.Options;

public sealed record ApplicationOptions
{
    public required string ApplicationName { get; init; }
    public required string CompanyName { get; init; }
}
