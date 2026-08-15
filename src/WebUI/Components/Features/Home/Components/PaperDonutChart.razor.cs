using Vioren.CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Home.Components;

public partial class PaperDonutChart
{
    [Parameter, EditorRequired]
    public IReadOnlyList<ProjectDto> Projects { get; set; }

    private static readonly DonutChartOptions _options = new()
    {
        ShowValues = true,
    };

    private IEnumerable<ProjectDto> SelectedProjects => Projects
        .Where(project => project.DocumentsCount > 0);

    private string[] Labels => SelectedProjects
        .Select(project => project.Title).ToArray();

    private List<ChartSeries<double>> Series => [
            new ChartSeries<double>
            {
                Name = $"{DomainDisplayTextFor.Documents} {DomainDisplayTextFor.Count}",
                Data =  SelectedProjects
                    .Select(p => (double)p.DocumentsCount).ToList()
            }
        ];
}
