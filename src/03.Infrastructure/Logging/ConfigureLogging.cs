using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Debugging;

namespace Vioren.CodebaseExpress.Infrastructure.Logging;

public static class ConfigureLogging
{
    public static void AddSerilogLogging(this WebApplicationBuilder builder)
    {
        _ = builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration));

        SelfLog.Enable(Console.WriteLine);
    }
}
