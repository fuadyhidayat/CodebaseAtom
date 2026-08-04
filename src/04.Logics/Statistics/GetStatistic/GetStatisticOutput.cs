namespace Vioren.CodebaseExpress.Logics.Statistics.GetStatistic;

public sealed record GetStatisticOutput
{
    public required IReadOnlyList<ProjectDto> Projects { get; init; }
}
