using Vioren.CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Home.Components;

public partial class PaperDonutChart
{
    [Parameter, EditorRequired]
    public IReadOnlyList<ProjectDto> Projects { get; set; }

    private int _index = -1;
    private IReadOnlyList<ProjectDto> _selectedProjects = [];
    private IEnumerable<string> _labels = [];
    private IReadOnlyCollection<ChartSeries<double>> _data = [];

    protected override void OnParametersSet()
    {
        _selectedProjects = Projects.Where(project => project.DocumentsCount > 0).ToList();
        _labels = _selectedProjects.Select(project => project.Title);
        _data = _selectedProjects.Select(project => new ChartSeries<double>
        {
            Name = project.Title,
            Data = new List<double> { project.DocumentsCount }
        }).ToList();
    }
}
