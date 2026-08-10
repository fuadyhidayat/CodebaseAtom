using System.Reflection;

namespace Vioren.CodebaseAtom.WebUI.Common.Statics;

public static class CommonValueFor
{
    private static readonly Assembly _assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

    public static string EnvironmentName { get; } = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown";
    public static DateTime AssemblyLastBuild { get; } = File.GetLastWriteTime(_assembly.Location);

    public static string InformationalVersion
    {
        get
        {
            var version = _assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            if (string.IsNullOrEmpty(version))
            {
                return "Unknown";
            }

            var plusIndex = version.IndexOf('+');

            if (plusIndex >= 0)
            {
                var baseVersion = version[..plusIndex];
                var commitHash = version[(plusIndex + 1)..];

                if (commitHash.Length > 8)
                {
                    commitHash = commitHash[..8];
                }

                return $"{baseVersion}+{commitHash}";
            }

            return version;
        }
    }
}
