using Swashbuckle.AspNetCore.Filters;

namespace Sandbox.Api.Web.Errors;

/// <summary>
/// Provider of example data for a <see cref="ApplicationErrorModel"/> 
/// </summary>
public class ApplicationErrorModelExampleProvider : IExamplesProvider<ApplicationErrorModel>
{
    /// <summary>
    /// Returns the example data for <see cref="ApplicationErrorModel"/>
    /// </summary>
    /// <returns></returns>
    public ApplicationErrorModel GetExamples()
    {
        return new ApplicationErrorModel("Error message", new Dictionary<string, string>
        {
            { "Field1", "Additional Information" },
            { "Field2", "More Context" }
        });
    }
}