using System.ComponentModel.DataAnnotations;

namespace Sandbox.Api.Data.Models.Entities;

/// <summary>
/// Database entity representing an address
/// </summary>
public class Address : BaseEntity
{
    /// <summary>
    /// Numeric representation of the type of address
    /// </summary>
    /// <seealso cref="Enums.AddressType"/>
    public int AddressType { get; set; }

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