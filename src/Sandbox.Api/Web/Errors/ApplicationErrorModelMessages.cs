namespace Sandbox.Api.Web.Errors;

/// <summary>
/// Class containing predefined error messages for <see cref="ApplicationErrorModel"/> objects
/// </summary>
public static class ApplicationErrorModelMessages
{
    /// <summary>
    /// Error message to be provided when a resource was not found
    /// </summary>
    public const string NotFound = "Object not found";

    /// <summary>
    /// Error message to be provided when an ETag conflict was detected
    /// </summary>
    public const string Conflict = "Concurrency error";

    /// <summary>
    /// Error message to be provided when a generic exception is caught
    /// </summary>
    public const string InternalError = "An internal server error has occurred";
}