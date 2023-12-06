namespace Sandbox.Api.Common.Exceptions;

/// <summary>
/// Represents an error that occurs when a requested database entity is not found
/// </summary>
public class NotFoundException : ApplicationException
{
    public string ResourceType { get; set; }
    
    public Guid Id { get; set; }
    
    public NotFoundException(string type, Guid id) 
        : base ($"{type} not found matching Id \"{id}\"")
    {
        Id = id;
        ResourceType = type;
    }
}