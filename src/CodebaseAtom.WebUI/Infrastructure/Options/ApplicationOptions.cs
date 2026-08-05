namespace CodebaseAtom.WebUI.Infrastructure.Options;

public sealed record ApplicationOptions
{
    public const string SectionKey = "Application";

    public required string ApplicationName { get; init; }
    public required string CompanyName { get; init; }
}
