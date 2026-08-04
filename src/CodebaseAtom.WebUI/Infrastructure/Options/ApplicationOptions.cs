namespace CodebaseAtom.WebUI.Infrastructure.Options;

public sealed record ApplicationOptions
{
    public required string ApplicationName { get; init; }
    public required string CompanyName { get; init; }
}
