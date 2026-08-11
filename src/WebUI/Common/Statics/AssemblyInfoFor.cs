using System.Reflection;

namespace Vioren.CodebaseAtom.WebUI.Common.Statics;

public static class AssemblyInfoFor
{
    public static string EnvironmentName => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown";

    public static DateTime AssemblyLastBuild
    {
        get
        {
            var path = Assembly.GetExecutingAssembly().Location;

            if (string.IsNullOrEmpty(path))
            {
                path = AppContext.BaseDirectory;
            }

            return File.GetLastWriteTime(path);
        }
    }

    public static string InformationalVersion
    {
        get
        {
            var version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

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
