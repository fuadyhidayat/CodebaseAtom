namespace CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;

public sealed record ProjectDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required int WorkItemsCount { get; init; }
    public required int DocumentsCount { get; init; }
}
