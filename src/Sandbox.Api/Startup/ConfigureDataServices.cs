using Sandbox.Api.Data;

namespace Sandbox.Api.Startup;

public static class ConfigureDataServices
{
    public static WebApplicationBuilder AddDataServices(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureServices(builder.Configuration);

        return builder;
    }
}