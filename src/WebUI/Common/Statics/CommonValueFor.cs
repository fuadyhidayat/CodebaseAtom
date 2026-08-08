using System.Reflection;

namespace Vioren.CodebaseAtom.WebUI.Common.Statics;

public static class CommonValueFor
{
    private static readonly Assembly _assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

    public static string EnvironmentName { get; } = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown";
    public static DateTime AssemblyLastBuild { get; } = File.GetLastWriteTime(_assembly.Location);
    public static string SemanticVersion { get; } = GenerateSemanticVersion();

    private static string GenerateSemanticVersion()
    {
        var version = _assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

        if (string.IsNullOrWhiteSpace(version))
        {
            return "Unknown";
        }

        var plusIndex = version.IndexOf('+');

        return plusIndex < 0 ? version : version[..plusIndex];
    }
}
