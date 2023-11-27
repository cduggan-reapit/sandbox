using Asp.Versioning;

namespace Sandbox.Api.Startup;

public static class ConfigureWebServices
{
    public static WebApplicationBuilder AddWebServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        }).AddApiExplorer(options =>
        {
            // ReSharper disable once StringLiteralTypo
            options.GroupNameFormat = "'v'VV";
            options.SubstituteApiVersionInUrl = true;
        });
        
        builder.Services.Configure<RouteOptions>(opts => opts.LowercaseUrls = true);
        builder.Services.AddControllers();
        
        return builder;
    }
}