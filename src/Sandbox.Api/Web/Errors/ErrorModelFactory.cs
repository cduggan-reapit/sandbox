using FluentValidation.Results;
using static Sandbox.Api.Web.Errors.ErrorModelMessages;

namespace Sandbox.Api.Web.Errors;

/// <summary>
/// Factory to create instances of <see cref="ErrorModel" />
/// </summary>
public static class ErrorModelFactory
{
    /// <summary>
    /// Creates an ErrorModel from a collection of validation failures
    /// </summary>
    /// <param name="errors">A collection of <see cref="ValidationFailure"/></param>
    /// <returns>A new <see cref="ErrorModel" /> representing the validation failures.</returns>
    public static ErrorModel GetErrorModelFromValidationResult(IEnumerable<ValidationFailure> errors)
        => new (Message: ValidationFailed, Errors: errors.GroupBy(e => e.PropertyName).ToDictionary(
                keySelector: g => g.Key,
                elementSelector: g => g.Select(e => e.ErrorMessage).ToArray()));
}