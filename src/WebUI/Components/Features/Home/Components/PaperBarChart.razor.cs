using Vioren.CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Home.Components;

public partial class PaperBarChart
{
    [Parameter, EditorRequired]
    public IReadOnlyList<ProjectDto> Projects { get; set; }

    private static readonly BarChartOptions _options = new()
    {
        ShowValues = true,
        XAxisTitle = $"{DomainDisplayTextFor.Projects}",
        XAxisLabelRotation = 45,
        YAxisTicks = 10,
        YAxisTitle = $"{DomainDisplayTextFor.WorkItems} {DomainDisplayTextFor.Count}"
    };

    private List<ChartSeries<double>> Series => Projects
        .Where(project => project.WorkItemsCount > 0)
        .Select(project => new ChartSeries<double>
        {
            Name = project.Title,
            Data = new double[] { project.WorkItemsCount }
        })
        .ToList();
}
