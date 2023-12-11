namespace Sandbox.Api.Web.Controllers.Addresses.V1.Models;

/// <summary>
/// Write model for Addresses 
/// </summary>
/// <param name="AddressType">The type of address (unknown/residential/commercial) [unknown/residential/commercial]</param>
/// <param name="Number">Building name/number</param>
/// <param name="Street">The name of the street on which the building is located</param>
/// <param name="City">The name of the town/city in which the building is located</param>
/// <param name="County">The name of the county in which the building is located</param>
/// <param name="State">The name of the state in which the building is located</param>
/// <param name="Country">The name of the country in which the building is located</param>
/// <param name="PostCode">The zip/post code for the building</param>
public record WriteAddressRequestModel(
    string AddressType, 
    string Number, 
    string Street, 
    string City, 
    string? County, 
    string? State, 
    string Country, 
    string PostCode);