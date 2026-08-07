using CodebaseAtom.WebUI.Components.Features.Projects.Pages;

namespace CodebaseAtom.WebUI.Components.Features.Projects.Components;

public partial class TabContentGeneralInfo
{
    [Parameter, EditorRequired]
    public ProjectModel Project { get; set; }
}
