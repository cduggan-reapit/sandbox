namespace Sandbox.Api.Web.Errors.Application;

/// <summary>
/// User-facing model containing details of an application error
/// </summary>
/// <param name="Message">A brief description of the type of error that has occurred</param>
/// <param name="Details">A dictionary containing information relating to the error</param>
public record ApplicationErrorModel(string Message, Dictionary<string, string> Details);