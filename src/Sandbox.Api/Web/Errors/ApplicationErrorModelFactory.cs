using Sandbox.Api.Common.Exceptions;
using static Sandbox.Api.Web.Errors.ApplicationErrorModelMessages;

namespace Sandbox.Api.Web.Errors;

/// <summary>
/// Methods for the creation of <see cref="ApplicationErrorModel"/> objects
/// </summary>
public static class ApplicationErrorModelFactory
{
    /// <summary>
    /// Extension method to create a new <see cref="ApplicationErrorModel"/> from a NotFoundException
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public static ApplicationErrorModel GetNotFoundErrorModel(this NotFoundException exception)
        => GetNotFoundErrorModel(exception.Message, exception.ResourceType, exception.Id.ToString());

    /// <summary>
    /// Create a new <see cref="ApplicationErrorModel"/> using NotFoundException details 
    /// </summary>
    /// <param name="description">A description of the error that occurred</param>
    /// <param name="type">The type of the resource being requested</param>
    /// <param name="id">The provided identifier</param>
    /// <returns></returns>
    public static ApplicationErrorModel GetNotFoundErrorModel(string description, string type, string id)
        => new (NotFound, new Dictionary<string, string>
        {
            { "Description", description },
            { "Type", type },
            { "Id", id },
        });

    /// <summary>
    /// Extension method to create a new <see cref="ApplicationErrorModel"/> from an EntityConflictException
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public static ApplicationErrorModel GetConflictErrorModel(this EntityConflictException exception)
        => GetConflictErrorModel(exception.Message, exception.ExpectedETag, exception.ActualETag);

    /// <summary>
    /// Create a new <see cref="ApplicationErrorModel"/> using EntityConflictException details 
    /// </summary>
    /// <param name="description">A description of the error that occurred</param>
    /// <param name="expected">The ETag of the current resource</param>
    /// <param name="actual">The ETag provided in the request</param>
    /// <returns></returns>
    public static ApplicationErrorModel GetConflictErrorModel(string description, string expected, string actual)
        => new (Conflict, new Dictionary<string, string>
        {
            { "Description", description },
            { "Expected", expected },
            { "Actual", actual },
        });
    
    /// <summary>
    /// Extension method to create a new <see cref="ApplicationErrorModel"/> from an Exception
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public static ApplicationErrorModel GetExceptionErrorModel(this Exception exception)
        => GetExceptionErrorModel(exception.Message, exception.GetType().Name, exception.HResult.ToString());

    /// <summary>
    /// Create a new <see cref="ApplicationErrorModel"/> using Exception details 
    /// </summary>
    /// <param name="description">A description of the error that occurred</param>
    /// <param name="type">The type of the caught exception</param>
    /// <param name="hResult">The HResult provided in the exception</param>
    /// <returns></returns>
    public static ApplicationErrorModel GetExceptionErrorModel(string description, string type, string hResult)
        => new(InternalError, new Dictionary<string, string>
        {
            { "Description", description },
            { "Type", type },
            { "HResult", hResult }
        });
}