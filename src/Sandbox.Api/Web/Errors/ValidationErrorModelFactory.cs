using FluentValidation.Results;
using static Sandbox.Api.Web.Errors.ValidationErrorModelMessages;

namespace Sandbox.Api.Web.Errors;

/// <summary>
/// Factory to create instances of <see cref="ValidationErrorModel" />
/// </summary>
public static class ValidationErrorModelFactory
{
    /// <summary>
    /// Creates an ErrorModel from a collection of validation failures
    /// </summary>
    /// <param name="errors">A collection of <see cref="ValidationFailure"/></param>
    /// <returns>A new <see cref="ValidationErrorModel" /> representing the validation failures.</returns>
    public static ValidationErrorModel GetValidationErrorModel(this IEnumerable<ValidationFailure> errors)
        => new(Message: ValidationFailed, Errors: GetValidationErrorDictionary(errors));

    /// <summary>
    /// Convert a collection of <see cref="ValidationFailure"/> into a dictionary of errors
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    private static Dictionary<string, string[]> GetValidationErrorDictionary(IEnumerable<ValidationFailure> errors)
        => errors.GroupBy(e => e.PropertyName)
            .ToDictionary(
                keySelector: g => g.Key,
                elementSelector: g => g.Select(e => e.ErrorMessage).ToArray());
}