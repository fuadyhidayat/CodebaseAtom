namespace CodebaseAtom.WebUI.Components.Features.Projects.Statics;

public static class RouteFor
{
    public const string Index = "Projects";

    public static string ProjectDetails(Guid projectId)
    {
        return $"{Index}/Details/{projectId}";
    }
}
