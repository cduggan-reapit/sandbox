namespace Sandbox.Api.Web.Errors;

/// <summary>
/// User-facing model containing details of an error
/// </summary>
/// <param name="Message">A brief description of the type of error that has occurred</param>
/// <param name="Errors">A dictionary of errors for each property to fail validation</param>
public record ValidationErrorModel(string Message, Dictionary<string, string[]> Errors);