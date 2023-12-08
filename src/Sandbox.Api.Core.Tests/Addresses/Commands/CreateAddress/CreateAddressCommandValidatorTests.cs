using Sandbox.Api.Core.Addresses.Commands.CreateAddress;
using static Sandbox.Api.Core.Tests.Addresses.Commands.CreateAddress.CreateAddressTestHelpers;
using static Sandbox.Api.Core.Addresses.Commands.CreateAddress.CreateAddressCommandErrors;

namespace Sandbox.Api.Core.Tests.Addresses.Commands.CreateAddress;

public class CreateAddressCommandValidatorTests
{
    [Fact]
    public async Task Validation_ShouldPass_WhenValidCommandProvided()
    {
        var command = GetTestCommand();

        var sut = CreateSut();

        var result = await sut.ValidateAsync(command);

        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public async Task Validation_ShouldFail_WhenInvalidAddressTypeProvided()
    {
        const string addressType = "invalid";
        var command = GetTestCommand(addressType: addressType);

        var sut = CreateSut();

        var result = await sut.ValidateAsync(command);

        result.ShouldHaveOneErrorWithMessage(nameof(CreateAddressCommand.AddressType), AddressTypeInvalid);
    }
    
    [Fact]
    public async Task Validation_ShouldFail_WhenNumberEmpty()
    {
        var command = GetTestCommand(number: string.Empty);
        var sut = CreateSut();
        var result = await sut.ValidateAsync(command);
        result.ShouldHaveOneErrorWithMessage(nameof(CreateAddressCommand.Number), NumberRequired);
    }
    
    [Fact]
    public async Task Validation_ShouldFail_WhenNumberExceedsMaxLength()
    {
        var command = GetTestCommand(number: new string('-', 101));
        var sut = CreateSut();
        var result = await sut.ValidateAsync(command);
        result.ShouldHaveOneErrorWithMessage(nameof(CreateAddressCommand.Number), NumberExceedsMaxLength);
    }
    
    [Fact]
    public async Task Validation_ShouldFail_WhenStreetEmpty()
    {
        var command = GetTestCommand(street: string.Empty);
        var sut = CreateSut();
        var result = await sut.ValidateAsync(command);
        result.ShouldHaveOneErrorWithMessage(nameof(CreateAddressCommand.Street), StreetRequired);
    }
    
    [Fact]
    public async Task Validation_ShouldFail_WhenStreetExceedsMaxLength()
    {
        var command = GetTestCommand(street: new string('-', 501));
        var sut = CreateSut();
        var result = await sut.ValidateAsync(command);
        result.ShouldHaveOneErrorWithMessage(nameof(CreateAddressCommand.Street), StreetExceedsMaxLength);
    }
    
    [Fact]
    public async Task Validation_ShouldFail_WhenCityEmpty()
    {
        var command = GetTestCommand(city: string.Empty);
        var sut = CreateSut();
        var result = await sut.ValidateAsync(command);
        result.ShouldHaveOneErrorWithMessage(nameof(CreateAddressCommand.City), CityRequired);
    }
    
    [Fact]
    public async Task Validation_ShouldFail_WhenCityExceedsMaxLength()
    {
        var command = GetTestCommand(city: new string('-', 101));
        var sut = CreateSut();
        var result = await sut.ValidateAsync(command);
        result.ShouldHaveOneErrorWithMessage(nameof(CreateAddressCommand.City), CityExceedsMaxLength);
    }
    
    [Fact]
    public async Task Validation_ShouldFail_WhenCountyExceedsMaxLength()
    {
        var command = GetTestCommand(county: new string('-', 101));
        var sut = CreateSut();
        var result = await sut.ValidateAsync(command);
        result.ShouldHaveOneErrorWithMessage(nameof(CreateAddressCommand.County), CountyExceedsMaxLength);
    }
    
    [Fact]
    public async Task Validation_ShouldFail_WhenStateExceedsMaxLength()
    {
        var command = GetTestCommand(state: new string('-', 101));
        var sut = CreateSut();
        var result = await sut.ValidateAsync(command);
        result.ShouldHaveOneErrorWithMessage(nameof(CreateAddressCommand.State), StateExceedsMaxLength);
    }
    
    [Fact]
    public async Task Validation_ShouldFail_WhenCountryEmpty()
    {
        var command = GetTestCommand(country: string.Empty);
        var sut = CreateSut();
        var result = await sut.ValidateAsync(command);
        result.ShouldHaveOneErrorWithMessage(nameof(CreateAddressCommand.Country), CountryRequired);
    }
    
    [Fact]
    public async Task Validation_ShouldFail_WhenCountryExceedsMaxLength()
    {
        var command = GetTestCommand(country: new string('-', 101));
        var sut = CreateSut();
        var result = await sut.ValidateAsync(command);
        result.ShouldHaveOneErrorWithMessage(nameof(CreateAddressCommand.Country), CountryExceedsMaxLength);
    }
    
    [Fact]
    public async Task Validation_ShouldFail_WhenPostcodeEmpty()
    {
        var command = GetTestCommand(postCode: string.Empty);
        var sut = CreateSut();
        var result = await sut.ValidateAsync(command);
        result.ShouldHaveOneErrorWithMessage(nameof(CreateAddressCommand.PostCode), PostcodeRequired);
    }
    
    [Fact]
    public async Task Validation_ShouldFail_WhenPostcodeExceedsMaxLength()
    {
        var command = GetTestCommand(postCode: new string('-', 51));
        var sut = CreateSut();
        var result = await sut.ValidateAsync(command);
        result.ShouldHaveOneErrorWithMessage(nameof(CreateAddressCommand.PostCode), PostcodeExceedsMaxLength);
    }
    
    private static CreateAddressCommandValidator CreateSut() => new();
}