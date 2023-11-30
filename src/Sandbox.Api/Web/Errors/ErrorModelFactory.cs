using FluentValidation;
using FluentValidation.Results;

namespace Sandbox.Api.Web.Errors;

public static class ErrorModelFactory
{
    public static ErrorModel GetErrorModel(IEnumerable<ValidationFailure> errors)
        => new (Message: "Validation failure", Errors: errors.GroupBy(e => e.PropertyName).ToDictionary(
                keySelector: g => g.Key,
                elementSelector: g => g.Select(e => e.ErrorMessage).ToArray()));
}