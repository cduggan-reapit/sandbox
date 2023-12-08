using FluentValidation.Results;

namespace Sandbox.Api.Core.Tests.TestHelpers;

public static class FluentValidationResultExtensions
{
    public static void ShouldHaveOneErrorWithMessage(this ValidationResult result, string propertyName, string expectedMessage)
    {
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);

        var error = result.Errors.First();
        error.PropertyName.Should().Be(propertyName);
        error.ErrorMessage.Should().Be(expectedMessage);
    }
}