using FluentAssertions;
using Sandbox.Api.Common.Helpers;

namespace Sandbox.Api.Common.Tests.Helpers;

public class CaseInsensitiveStringEqualityComparerTests
{
    [Fact]
    public void Equals_ShouldReturnTrue_WhenValuesBothNull()
    {
        var comparer = new CaseInsensitiveStringEqualityComparer();

        var result = comparer.Equals(null, null);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("a", null)]
    [InlineData(null, "b")]
    public void Equals_ShouldReturnFalse_WhenOneValueNull(string? a, string? b)
    {
        var comparer = new CaseInsensitiveStringEqualityComparer();

        var result = comparer.Equals(a, b);

        result.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("a", "A")]
    [InlineData("A", "a")]
    [InlineData("A", "A")]
    [InlineData("a", "a")]
    public void Equals_ShouldReturnFalse_WhenValuesEqual(string? a, string? b)
    {
        var comparer = new CaseInsensitiveStringEqualityComparer();

        var result = comparer.Equals(a, b);

        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("a", "b")]
    [InlineData("a", "B")]
    [InlineData("A", "b")]
    [InlineData("A", "B")]
    public void Equals_ShouldReturnFalse_WhenValuesDifferent(string? a, string? b)
    {
        var comparer = new CaseInsensitiveStringEqualityComparer();

        var result = comparer.Equals(a, b);

        result.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldThrowArgumentNullException_WhenObjectNull()
    {
        var comparer = new CaseInsensitiveStringEqualityComparer();
        var action = () => comparer.GetHashCode(null!);
        action.Should().Throw<ArgumentNullException>();
    }
    
    [Fact]
    public void GetHashCode_ShouldReturnHashCode_WhenStringProvided()
    {
        var comparer = new CaseInsensitiveStringEqualityComparer();

        const string input = "i'm a test!;";
        var expected = input.GetHashCode();

        var actual = comparer.GetHashCode(input);

        actual.Should().Be(expected);
    }
}