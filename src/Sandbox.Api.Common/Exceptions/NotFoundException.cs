namespace Sandbox.Api.Common.Exceptions;

/// <summary>
/// Represents an error that occurs when a requested database entity is not found
/// </summary>
public class NotFoundException : Exception
{
    public Type ResourceType { get; set; }
    
    public Guid Id { get; set; }
    
    public NotFoundException(Type type, Guid id)
    {
        Id = id;
        ResourceType = type;
    }
}