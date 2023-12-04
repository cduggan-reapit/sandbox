using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sandbox.Api.Data.Context;
using Sandbox.Api.Data.Repositories;

namespace Sandbox.Api.Data;

public static class Startup
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connstring = configuration.GetConnectionString("Sqlite");
        
        services.AddDbContext<SandboxDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("Sqlite")));

        services.AddScoped<IAddressRepository, AddressRepository>();
        
        return services;
    }
}