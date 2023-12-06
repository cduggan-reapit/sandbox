namespace Sandbox.Api.Web.Controllers.Addresses.V1.Models;

/// <summary>
/// Read model for Addresses
/// </summary>
/// <param name="Id"></param>
/// <param name="AddressType"></param>
/// <param name="Number"></param>
/// <param name="Street"></param>
/// <param name="City"></param>
/// <param name="County"></param>
/// <param name="State"></param>
/// <param name="Country"></param>
/// <param name="PostCode"></param>
/// <param name="Created"></param>
/// <param name="Modified"></param>
public record ReadAddressResponseModel(
    Guid Id, 
    string AddressType, 
    string Number, 
    string Street, 
    string City, 
    string? County,
    string? State, 
    string Country, 
    string PostCode, 
    DateTimeOffset Created, 
    DateTimeOffset Modified);