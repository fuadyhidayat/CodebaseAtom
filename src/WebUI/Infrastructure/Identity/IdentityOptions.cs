namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

public sealed record IdentityOptions
{
    public const string SectionKey = "Identity";

    public required string ConnectionString { get; init; }
    public required string DefaultPasswordForInitialUsers { get; init; }
}
