using Sandbox.Api.Web.Errors.Application;

namespace Sandbox.Api.Tests.Web.Errors.Application;

public class ApplicationErrorModelExampleProviderTests
{
    [Fact]
    public void GetExamples_ShouldReturn_ApplicationErrorModel()
    {
        var provider = new ApplicationErrorModelExampleProvider();
        var example = provider.GetExamples();
        example.Should().BeOfType<ApplicationErrorModel>();
    }
}