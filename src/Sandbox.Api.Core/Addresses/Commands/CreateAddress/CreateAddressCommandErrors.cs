namespace Sandbox.Api.Core.Addresses.Commands.CreateAddress;

public static class CreateAddressCommandErrors
{
    public const string AddressTypeInvalid = "AddressTypeInvalid";
    public const string NumberRequired = "NumberRequired";
    public const string NumberExceedsMaxLength = "NumberExceedsMaxLength";
    public const string StreetRequired = "StreetRequired";
    public const string StreetExceedsMaxLength = "StreetExceedsMaxLength";
    public const string CityRequired = "CityRequired";
    public const string CityExceedsMaxLength = "CityExceedsMaxLength";
    public const string CountyExceedsMaxLength = "CountyExceedsMaxLength";
    public const string StateExceedsMaxLength = "StateExceedsMaxLength";
    public const string CountryRequired = "CountryRequired";
    public const string CountryExceedsMaxLength = "CountryExceedsMaxLength";
    public const string PostcodeRequired = "PostcodeRequired";
    public const string PostcodeExceedsMaxLength = "PostcodeExceedsMaxLength";
}