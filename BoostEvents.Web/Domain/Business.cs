using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class Business : BaseEntity
{
    [Required]
    public Guid TenantId { get; set; }

    [Required, MaxLength(200)]
    public required string Name { get; set; }

    public ICollection<BusinessInstance>? Instances { get; set; }

    public ICollection<AuditLog>? AuditLogs { get; set; }

    public ICollection<AiLog>? AiLogs { get; set; }
}