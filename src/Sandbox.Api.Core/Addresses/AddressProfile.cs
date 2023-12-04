using AutoMapper;
using Sandbox.Api.Core.Addresses.Commands.CreateAddress;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Core.Addresses.Enums;
using Sandbox.Api.Data.Entities;
using Sandbox.Api.Data.Helpers;

namespace Sandbox.Api.Core.Addresses;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        // Map from database entity to read dto
        CreateMap<Address, ReadAddressDto>()
            .ForMember(dest => dest.EntityTag, op => op.MapFrom(src => src.GenerateEtagForEntity()))
            .ForMember(dest => dest.AddressType, op => op.MapFrom(src => (AddressType)src.AddressType));
        
        // Map from Create command to database entity
        CreateMap<CreateAddressCommand, Address>()
            .ForMember(dest => dest.AddressType, 
                op => op.MapFrom(src => (AddressType)src.AddressType));
    }
}