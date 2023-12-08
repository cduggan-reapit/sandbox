using FluentValidation.Results;
using Sandbox.Api.Web.Errors.Validation;
using static Sandbox.Api.Web.Errors.Validation.ValidationErrorModelMessages;

namespace Sandbox.Api.Tests.Web.Errors.Validation;

public class ValidationErrorModelFactoryTests
{
    [Fact]
    public void GetErrorModel_ShouldCreateErrorModel_FromGivenValidationFailures()
    {
        var input = new[]
        {
            new ValidationFailure { PropertyName = "Property One", ErrorMessage = "Error One" },
            new ValidationFailure { PropertyName = "Property One", ErrorMessage = "Error Two" },
            new ValidationFailure { PropertyName = "Property Two", ErrorMessage = "Error Three" }
        };

        var expectedResult = new ValidationErrorModel(
            Message: ValidationFailed,
            Errors: new Dictionary<string, string[]>
            {
                { "Property One", new [] { "Error One", "Error Two" } },
                { "Property Two", new [] { "Error Three" } }
            });

        var actual = input.GetValidationErrorModel();

        actual.Should().BeEquivalentTo(expectedResult);
    }
}