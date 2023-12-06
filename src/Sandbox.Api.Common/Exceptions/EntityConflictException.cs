namespace Sandbox.Api.Common.Exceptions;

/// <summary>
/// Represents an error that occurs when the provided ETag differs from that of the current entity 
/// </summary>
public class EntityConflictException : ApplicationException
{
    /// <summary>
    /// The current ETag of the entity
    /// </summary>
    public string ExpectedETag { get; set; }

    /// <summary>
    /// The provided ETag 
    /// </summary>
    public string ActualETag { get; set; }
    
    /// <summary>
    /// Initialize a new <see cref="EntityConflictException"/>
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="actual"></param>
    public EntityConflictException(string expected, string actual)
        : base($"ETag Conflict.  Expected {expected} / Actual {actual}")
    {
        ExpectedETag = expected;
        ActualETag = actual;
    }
}