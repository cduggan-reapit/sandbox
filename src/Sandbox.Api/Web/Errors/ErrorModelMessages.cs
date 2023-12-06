namespace Sandbox.Api.Web.Errors;

/// <summary>
/// List of standard error messages for use in <see cref="ErrorModel"/>
/// </summary>
public static class ErrorModelMessages
{
    /// <summary>
    /// Error message to return when validation has failed
    /// </summary>
    public const string ValidationFailed = "Validation failure";

    /// <summary>
    /// Error message to return when an internal server error has occurred
    /// </summary>
    public const string InternalServerError = "Internal server error";

    /// <summary>
    /// Error message to return when a requested entity is not found
    /// </summary>
    public const string NotFound = "Object not found";

    /// <summary>
    /// Error message to return when the provided ETag differs from the current entity ETag
    /// </summary>
    public const string Conflict = "Conflict";
}