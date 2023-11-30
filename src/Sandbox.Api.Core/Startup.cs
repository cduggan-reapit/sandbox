using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Sandbox.Api.Core;

public static class Startup
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));

        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
        
        return services;
    }
}