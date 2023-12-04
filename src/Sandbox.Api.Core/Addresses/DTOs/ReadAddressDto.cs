using Sandbox.Api.Core.Addresses.Enums;
using Sandbox.Api.Core.DTOs;

namespace Sandbox.Api.Core.Addresses.DTOs;

public class ReadAddressDto : BaseReadDto
{
    /// <summary>
    /// The type of address (residential/commercial/unknown)
    /// </summary>
    public AddressType AddressType { get; set; } = AddressType.Unknown;

    /// <summary>
    /// Building name/number
    /// </summary>
    public string Number { get; set; } = default!;

    /// <summary>
    /// The name of the street on which the building is located
    /// </summary>
    public string Street { get; set; } = default!;

    /// <summary>
    /// The name of the town/city in which the building is located
    /// </summary>
    public string City { get; set; } = default!;

    /// <summary>
    /// The name of the county in which the building is located
    /// </summary>
    public string? County { get; set; } = null;
    
    /// <summary>
    /// The name of the state in which the building is located
    /// </summary>
    public string? State { get; set; } = null;
    
    /// <summary>
    /// The name of the country in which the building is located
    /// </summary>
    public string Country { get; set; } = default!;
    
    /// <summary>
    /// The zip/post code for the building
    /// </summary>
    public string PostCode { get; set; } = default!;
}