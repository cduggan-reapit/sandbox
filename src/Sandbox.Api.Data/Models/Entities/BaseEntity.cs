using System.ComponentModel.DataAnnotations;

namespace Sandbox.Api.Data.Models.Entities;

public abstract class BaseEntity
{
    /// <summary>
    /// The auto-generated Id of an entity
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// The date & time on which an entity was created
    /// </summary>
    [Required]
    public DateTimeOffset Created { get; set; }
    
    /// <summary>
    /// The date & time on which an entity was last modified
    /// </summary>
    [Required]
    public DateTimeOffset Modified { get; set; }
}