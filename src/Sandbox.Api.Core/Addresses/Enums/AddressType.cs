namespace Sandbox.Api.Core.Addresses.Enums;

/// <summary>
/// Enum class mapping the integer AddressType value to readable strings
/// </summary>
public class AddressType
{
    /// <summary>
    /// The default Unknown AddressType value
    /// </summary>
    public static AddressType Unknown = new(0, "unknown");
    
    /// <summary>
    /// AddressType value representing Residential addresses
    /// </summary>
    public static AddressType Residential = new(1, "residential");
    
    /// <summary>
    /// AddressType value representing Commercial addresses
    /// </summary>
    public static AddressType Commercial = new(1, "commercial");
    
    /// <summary>
    /// The integer key representing an address type, as used by the database
    /// </summary>
    private int Key { get; init; }
    
    /// <summary>
    /// String description for an address type
    /// </summary>
    private string Value { get; init; }

    /// <summary>
    /// Collection of registered AddressType values 
    /// </summary>
    public static IList<AddressType> Values { get; } = new List<AddressType>();
    
    /// <summary>
    /// Initializes a new <see cref="AddressType"/>
    /// </summary>
    /// <param name="key"><inheritdoc cref="Key"/></param>
    /// <param name="displayName"></param>
    private AddressType(int key, string displayName)
    {
        Key = key;
        Value = displayName;
        Values.Add(this);
    }

    /// <summary>
    /// Convert a <see cref="AddressType"/> to <see cref="int"/>
    /// </summary>
    /// <param name="value">The AddressType to cast</param>
    /// <returns>An integer representation of the AddressType</returns>
    public static implicit operator int(AddressType value)
        => value.Key;
    
    /// <summary>
    /// Convert a <see cref="AddressType"/> to <see cref="string"/>
    /// </summary>
    /// <param name="value">The AddressType to cast</param>
    /// <returns>A string representation of the AddressType</returns>
    public static implicit operator string(AddressType value)
        => value.Value;
    
    /// <summary>
    /// Convert an <see cref="int"/> to <see cref="AddressType"/>
    /// </summary>
    /// <param name="key">The integer key of the AddressType</param>
    /// <returns>The AddressType registered to the provided key. Returns Unknown if the key is not registered.</returns>
    public static implicit operator AddressType(int key)
        => Values.SingleOrDefault(v => v.Key == key) ?? Unknown;
    
    /// <summary>
    /// Convert an <see cref="int"/> to <see cref="AddressType"/>
    /// </summary>
    /// <param name="value">The string value of the AddressType</param>
    /// <returns>The AddressType registered with the provided value. Returns Unknown if the value is not registered.</returns>
    public static implicit operator AddressType?(string value)
        => Values.SingleOrDefault(v => v.Value == value) ?? Unknown; 
}