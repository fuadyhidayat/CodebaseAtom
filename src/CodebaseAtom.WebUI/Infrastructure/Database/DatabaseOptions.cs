namespace CodebaseAtom.WebUI.Infrastructure.Database;

public sealed record DatabaseOptions
{
    public const string SectionKey = "Database";

    public required string ConnectionString { get; init; }
}
