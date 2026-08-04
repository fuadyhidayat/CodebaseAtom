using Serilog;
using Serilog.Debugging;

namespace CodebaseAtom.WebUI.Infrastructure.Logging;

public static class ConfigureLogging
{
    public static void AddSerilogLogging(this WebApplicationBuilder builder)
    {
        _ = builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration));

        SelfLog.Enable(Console.WriteLine);
    }
}
