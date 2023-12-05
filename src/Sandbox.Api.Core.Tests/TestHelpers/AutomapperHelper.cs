using AutoMapper;
using Sandbox.Api.Core.Addresses;

namespace Sandbox.Api.Core.Tests.TestHelpers;

public static class AutomapperHelper
{
    public static IMapper GetCoreMapper()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AddressProfile>();
        });
        return new Mapper(mapperConfig);
    }
}