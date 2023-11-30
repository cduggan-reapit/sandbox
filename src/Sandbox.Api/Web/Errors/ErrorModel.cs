namespace Sandbox.Api.Web.Errors;

public record ErrorModel(string Message, Dictionary<string, string[]> Errors);