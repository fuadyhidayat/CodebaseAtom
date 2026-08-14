using Vioren.CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Home.Components;

public partial class PaperDonutChart
{
    [Parameter, EditorRequired]
    public IReadOnlyList<ProjectDto> Projects { get; set; }

    private int _index = -1;
    private List<ProjectDto> _selectedProjects = [];
    private string[] _labels = [];
    private List<ChartSeries<double>> _data = [];

    protected override void OnParametersSet()
    {
        _selectedProjects = Projects.Where(project => project.DocumentsCount > 0).ToList();
        _labels = _selectedProjects.Select(project => project.Title).ToArray();

        _data =
        [
            new ChartSeries<double>
            {
                Name = $"{DomainDisplayTextFor.Documents} {DomainDisplayTextFor.Count}",
                Data = _selectedProjects.Select(p => (double)p.DocumentsCount).ToList()
            }
        ];
    }
}
