using Swashbuckle.AspNetCore.Filters;

namespace Sandbox.Api.Web.Errors.Validation;

/// <summary>
/// Provider of example data for a <see cref="ValidationErrorModel"/> 
/// </summary>
public class ValidationErrorModelExampleProvider : IExamplesProvider<ValidationErrorModel>
{
    /// <summary>
    /// Returns the example data for <see cref="ValidationErrorModel"/>
    /// </summary>
    /// <returns></returns>
    public ValidationErrorModel GetExamples()
    {
        return new ValidationErrorModel("Validation failure", new Dictionary<string, string[]>
        {
            { "Field1", new [] { "Description of the only validation error" } },
            { "Field2", new [] { "Description of the first validation error", "Description of the second validation error" } }
        });
    }
}