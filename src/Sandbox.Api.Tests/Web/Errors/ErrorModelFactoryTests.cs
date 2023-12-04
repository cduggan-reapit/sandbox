using FluentValidation.Results;
using Sandbox.Api.Web.Errors;
using static Sandbox.Api.Web.Errors.ErrorModelMessages;

namespace Sandbox.Api.Tests.Web.Errors;

public class ErrorModelFactoryTests
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

        var expectedResult = new ErrorModel(
            Message: ValidationFailed,
            Errors: new Dictionary<string, string[]>
            {
                { "Property One", new [] { "Error One", "Error Two" } },
                { "Property Two", new [] { "Error Three" } }
            });

        var actual = input.GetErrorModel();

        actual.Should().BeEquivalentTo(expectedResult);
    }
}