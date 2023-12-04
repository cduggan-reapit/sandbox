using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sandbox.Api.Data.Context;

/// <summary>
/// Design-time factory to allow EntityFrameworkCore.Tools and EntityFrameworkCore.Design to generate migrations 
/// </summary>
public class SandboxDbContextFactory : IDesignTimeDbContextFactory<SandboxDbContext>
{
    public SandboxDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SandboxDbContext>();
        optionsBuilder.UseSqlite("Filename=:memory:");

        return new SandboxDbContext(optionsBuilder.Options);
    }
}