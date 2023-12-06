using AutoMapper;
using Sandbox.Api.Web.Controllers.Addresses.V1;

namespace Sandbox.Api.Tests.TestHelpers;

public static class AutomapperHelper
{
    public static IMapper GetMapper()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AddressesProfile>();
        });
        return new Mapper(mapperConfig);
    }
}