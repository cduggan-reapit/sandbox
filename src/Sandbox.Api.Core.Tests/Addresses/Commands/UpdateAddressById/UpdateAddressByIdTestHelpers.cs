using Sandbox.Api.Core.Addresses.Commands.UpdateAddressById;

namespace Sandbox.Api.Core.Tests.Addresses.Commands.UpdateAddressById;

public static class UpdateAddressByIdTestHelpers
{
    public static UpdateAddressByIdCommand GetTestCommand(
        Guid? id = null,
        string etag = "etag",
        string addressType = "unknown",
        string number = "Suite 3206",
        string street = "353 N Desplaines St",
        string city = "Chicago",
        string? county = null,
        string? state = "IL",
        string country = "USA",
        string postCode = "60661")
        => new(
            Id: id ?? Guid.NewGuid(),
            ETag: etag,
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