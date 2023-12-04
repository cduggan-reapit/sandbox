using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Sandbox.Api.Core;

public static class Startup
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        // Register Mediatr Requests & Handlers in project
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        
        // Register Automapper profiles in project
        services.AddAutoMapper(typeof(Startup).Assembly);
        
        // Register validators in project
        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
        
        return services;
    }
}