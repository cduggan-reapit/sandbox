using MediatR;
using Sandbox.Api.Core.Addresses.DTOs;

namespace Sandbox.Api.Core.Addresses.Commands.UpdateAddressById;

/// <summary>
/// Command to update an Address 
/// </summary>
/// /// <param name="Id">The identifier of the address to update</param>
/// <param name="ETag">Concurrency check value for the address to update</param>
/// <param name="AddressType">The type of address</param>
/// <param name="Number">Building name/number</param>
/// <param name="Street">The name of the street on which the building is located</param>
/// <param name="City">The name of the town/city in which the building is located</param>
/// <param name="County">The name of the county in which the building is located</param>
/// <param name="State">The name of the state in which the building is located</param>
/// <param name="Country">The name of the country in which the building is located</param>
/// <param name="PostCode">The zip/post code for the building</param>
public record UpdateAddressByIdCommand(
    Guid Id,
    string ETag,
    string AddressType, 
    string Number, 
    string Street, 
    string City, 
    string? County, 
    string? State, 
    string Country, 
    string PostCode) : IRequest<ReadAddressDto>;