using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class AuditLog : BaseEntity
{
    [Required]
    public Guid BusinessInstanceId { get; set; }

    [Required]
    public AuditLogAction Action { get; set; }

    /// <summary>
    /// JSON payload of metadata describing the action context (e.g., user, device, ticket ID).
    /// </summary>
    [MaxLength(2048)]
    public required string MetadataJson { get; set; }
}