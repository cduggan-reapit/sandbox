using Sandbox.Api.Core.Addresses.Enums;

namespace Sandbox.Api.Core.Tests.Addresses.Enums;

public class AddressTypeTests
{
    [Fact]
    public void AddressType_ReturnsExpectedAddressType_WhenValidIntegerCast()
    {
        var expected = AddressType.Commercial;
        const int input = 2;

        AddressType addressType = input;
        addressType.Should().Be(expected);
    }
    
    [Fact]
    public void AddressType_ReturnsExpectedInteger_WhenAddressTypeCast()
    {
        const int expected = 1;
        var input = AddressType.Residential;

        int value = input;
        value.Should().Be(expected);
    }
    
    [Fact]
    public void AddressType_ReturnsUnknown_WhenInvalidIntegerCast()
    {
        var expected = AddressType.Unknown;
        const int input = -10;

        AddressType value = input;
        value.Should().Be(expected);
    }
    
    [Fact]
    public void AddressType_ReturnsExpectedAddressType_WhenValidStringCast()
    {
        var expected = AddressType.Residential;
        const string input = "residential";

        AddressType addressType = input;
        addressType.Should().Be(expected);
    }
    
    [Fact]
    public void AddressType_ReturnsExpectedString_WhenAddressTypeCast()
    {
        const string expected = "commercial";
        var input = AddressType.Commercial;

        string value = input;
        value.Should().Be(expected);
    }
    
    [Fact]
    public void AddressType_ReturnsUnknown_WhenInvalidStringCast()
    {
        var expected = AddressType.Unknown;
        const string input = "invalid";

        AddressType value = input;
        value.Should().Be(expected);
    }
}