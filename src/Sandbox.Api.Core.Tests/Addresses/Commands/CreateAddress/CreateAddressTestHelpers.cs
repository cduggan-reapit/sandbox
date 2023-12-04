using Sandbox.Api.Core.Addresses.Commands.CreateAddress;

namespace Sandbox.Api.Core.Tests.Addresses.Commands.CreateAddress;

public static class CreateAddressTestHelpers
{
    public static CreateAddressCommand GetTestCommand(
        string addressType = "unknown",
        string number = "Suite 3206",
        string street = "353 N Desplaines St",
        string city = "Chicago",
        string? county = null,
        string? state = "IL",
        string country = "USA",
        string postCode = "60661")
        => new(
            AddressType: addressType,
            Number: number,
            Street: street,
            City: city,
            County: county,
            State: state,
            Country: country,
            PostCode: postCode
        );
}