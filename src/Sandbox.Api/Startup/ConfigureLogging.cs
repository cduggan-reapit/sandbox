using Serilog;

namespace Sandbox.Api.Startup;

public static class ConfigureLogging
{
    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
            .ReadFrom.Configuration(context.Configuration));

        return builder;
    }
}