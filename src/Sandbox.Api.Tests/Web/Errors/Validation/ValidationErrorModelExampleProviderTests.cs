using Sandbox.Api.Web.Errors.Validation;

namespace Sandbox.Api.Tests.Web.Errors.Validation;

public class ValidationErrorModelExampleProviderTests
{
    [Fact]
    public void GetExamples_ShouldReturn_ApplicationErrorModel()
    {
        var provider = new ValidationErrorModelExampleProvider();
        var example = provider.GetExamples();
        example.Should().BeOfType<ValidationErrorModel>();
    }
}