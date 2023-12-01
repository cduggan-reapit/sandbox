using Swashbuckle.AspNetCore.Filters;

namespace Sandbox.Api.Web.Errors;

/// <summary>
/// Provider of example data for a <see cref="ErrorModel"/> 
/// </summary>
public class ErrorModelExampleProvider : IExamplesProvider<ErrorModel>
{
    /// <summary>
    /// Returns the example data for <see cref="ErrorModel"/>
    /// </summary>
    /// <returns></returns>
    public ErrorModel GetExamples()
    {
        return new ErrorModel("Validation failure", new Dictionary<string, string[]>
        {
            { "Field1", new [] { "Description of the only validation error" } },
            { "Field2", new [] { "Description of the first validation error", "Description of the second validation error" } }
        });
    }
}