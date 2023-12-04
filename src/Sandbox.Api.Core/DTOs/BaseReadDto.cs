namespace Sandbox.Api.Core.DTOs;

public abstract class BaseReadDto
{
    /// <summary>
    /// The auto-generated Id of the item
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The date & time on which the item entry was created
    /// </summary>
    public DateTimeOffset Created { get; set; }
    
    /// <summary>
    /// The date & time on which the item was last modified
    /// </summary>
    public DateTimeOffset Modified { get; set; }

    /// <summary>
    /// The identifier for a specific version of an item 
    /// </summary>
    public string EntityTag { get; set; } = default!;
}