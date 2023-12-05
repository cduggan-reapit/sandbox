using AutoMapper;
using Sandbox.Api.Core.Addresses.Commands.CreateAddress;
using Sandbox.Api.Web.Controllers.Addresses.V1.Models;

namespace Sandbox.Api.Web.Controllers.Addresses.V1;

public class AddressesProfile : Profile
{
    public AddressesProfile()
    {
        CreateMap<CreateAddressRequestModel, CreateAddressCommand>();
    }
}