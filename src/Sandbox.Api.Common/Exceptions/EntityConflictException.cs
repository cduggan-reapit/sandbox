namespace Sandbox.Api.Common.Exceptions;

/// <summary>
/// Represents an error that occurs when the provided ETag differs from that of the current entity 
/// </summary>
public class EntityConflictException : Exception
{
    public EntityConflictException(string message)
        : base(message)
    {
    }
}