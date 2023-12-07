using AutoMapper;
using Sandbox.Api.Core.Addresses.Commands.CreateAddress;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Web.Controllers.Addresses.V1.Models;

namespace Sandbox.Api.Web.Controllers.Addresses.V1;

public class AddressesProfile : Profile
{
    public AddressesProfile()
    {
        CreateMap<WriteAddressRequestModel, CreateAddressCommand>();
        CreateMap<ReadAddressDto, ReadAddressResponseModel>();
    }
}