using Vioren.CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Home.Components;

public partial class PaperBarChart
{
    [Parameter, EditorRequired]
    public IReadOnlyList<ProjectDto> Projects { get; set; }

    public IReadOnlyList<ProjectDto> SelectedProjects => Projects.Where(project => project.WorkItemsCount > 0).ToList();

    private BarChartOptions BlazorChartOptions => new()
    {
        ShowValues = true,
        XAxisLabelRotation = 45,
        YAxisLines = true,
        YAxisTicks = SelectedProjects.Max(project => project.WorkItemsCount) + 1
    };

    private List<ChartSeries<double>> WorkItemsCounts => SelectedProjects.Select(project => new ChartSeries<double>
    {
        Name = project.Title,
        Data = new double[] { project.WorkItemsCount }
    }).ToList();
}
