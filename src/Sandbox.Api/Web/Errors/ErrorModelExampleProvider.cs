using Swashbuckle.AspNetCore.Filters;

namespace Sandbox.Api.Web.Errors;

public class ErrorModelExampleProvider : IExamplesProvider<ErrorModel>
{
    public ErrorModel GetExamples()
    {
        return new ErrorModel("Validation failure", new Dictionary<string, string[]>
        {
            { "Field1", new [] { "Description of the only validation error" } },
            { "Field2", new [] { "Description of the first validation error", "Description of the second validation error" } }
        });
    }
}