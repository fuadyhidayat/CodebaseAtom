using Vioren.CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Home.Components;

public partial class PaperBarChart
{
    [Parameter, EditorRequired]
    public IReadOnlyList<ProjectDto> Projects { get; set; }

    private BarChartOptions _options = new()
    {
        ShowValues = true
    };

    private List<ProjectDto> _selectedProjects = [];
    private List<ChartSeries<double>> _workItemsCounts = [];

    protected override void OnParametersSet()
    {
        _selectedProjects = Projects.Where(project => project.WorkItemsCount > 0).ToList();

        _options = new()
        {
            ShowValues = true,
            XAxisLabelRotation = 45,
            YAxisTicks = _selectedProjects.Count > 0 ? _selectedProjects.Max(project => project.WorkItemsCount) : 0
        };

        // For Bar Chart with labels: Create one series per project
        _workItemsCounts = _selectedProjects.Select(project => new ChartSeries<double>
        {
            Name = project.Title,
            Data = new double[] { project.WorkItemsCount }
        }).ToList();
    }
}
