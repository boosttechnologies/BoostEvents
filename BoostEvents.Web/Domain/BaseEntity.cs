using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoostEvents.Web.Domain;

public abstract class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    
    public DateTime? ModifiedUtc { get; set; }
    
    public DateTime? DeletedUtc { get; set; }

    /*/// <summary>
    /// Used for optimistic concurrency control.
    /// </summary>
    [Timestamp]
    public byte[] RowVersion { get; set; }*/

    /// <summary>
    /// Optional tenant linkage for shared models.
    /// </summary>
    public Guid? TenantId { get; set; }

    [Required]
    public bool IsDeleted { get; set; } = false;

}