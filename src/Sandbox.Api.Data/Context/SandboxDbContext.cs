using Microsoft.EntityFrameworkCore;
using Sandbox.Api.Common.Helpers;
using Sandbox.Api.Data.Models.Entities;

namespace Sandbox.Api.Data.Context;

public class SandboxDbContext : DbContext
{
    public SandboxDbContext(DbContextOptions<SandboxDbContext> options)
        : base(options)
    {
    }

    public DbSet<Address> Addresses { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        PopulateBaseEntityCalculatedValues();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// Set Id, Created, and Modified, for new entities of type <see cref="BaseEntity"/>, updating the Modified property of modified entities 
    /// </summary>
    private void PopulateBaseEntityCalculatedValues()
    {
        foreach (var entity in ChangeTracker
                     .Entries()
                     .Where(x => x is { Entity: BaseEntity, State: EntityState.Added })
                     .Select(x => x.Entity)
                     .Cast<BaseEntity>())
        {
            entity.Id = Guid.NewGuid();
            entity.Created = DateTimeHelper.Now();
            entity.Modified = DateTimeHelper.Now();
        }
        
        foreach (var entity in ChangeTracker
                     .Entries()
                     .Where(x => x is { Entity: BaseEntity, State: EntityState.Modified })
                     .Select(x => x.Entity)
                     .Cast<BaseEntity>())
        {
            entity.Modified = DateTimeHelper.Now();
        }
    }
}