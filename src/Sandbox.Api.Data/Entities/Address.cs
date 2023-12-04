using System.ComponentModel.DataAnnotations;

namespace Sandbox.Api.Data.Entities;

/// <summary>
/// Database entity representing an address
/// </summary>
public class Address : BaseEntity
{
    /// <summary>
    /// Numeric representation of the type of address
    /// </summary>
    public int AddressType { get; set; }

    /// <summary>
    /// Building name/number
    /// </summary>
    [MaxLength(100)]
    public string Number { get; set; } = default!;

    /// <summary>
    /// The name of the street on which the building is located
    /// </summary>
    [MaxLength(500)]
    public string Street { get; set; } = default!;

    /// <summary>
    /// The name of the town/city in which the building is located
    /// </summary>
    [MaxLength(100)]
    public string City { get; set; } = default!;

    /// <summary>
    /// The name of the county in which the building is located
    /// </summary>
    [MaxLength(100)]
    public string? County { get; set; } = null;
    
    /// <summary>
    /// The name of the state in which the building is located
    /// </summary>
    [MaxLength(100)]
    public string? State { get; set; } = null;
    
    /// <summary>
    /// The name of the country in which the building is located
    /// </summary>
    [MaxLength(100)]
    public string Country { get; set; } = default!;
    
    /// <summary>
    /// The zip/post code for the building
    /// </summary>
    [MaxLength(50)]
    public string PostCode { get; set; } = default!;
}