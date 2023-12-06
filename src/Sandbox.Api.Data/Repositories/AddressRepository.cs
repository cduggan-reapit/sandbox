using Sandbox.Api.Data.Context;
using Sandbox.Api.Data.Entities;

namespace Sandbox.Api.Data.Repositories;

public class AddressRepository : BaseRepository<Address>, IAddressRepository
{
    public AddressRepository(SandboxDbContext dbContext)
        : base(dbContext)
    {
    }
}