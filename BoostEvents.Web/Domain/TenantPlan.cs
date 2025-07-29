using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoostEvents.Web.Domain;

public class TenantPlan : BaseEntity
{
    [Required]
    public Guid TenantId { get; set; }

    [Required, MaxLength(100)]
    public string PlanName { get; set; }

    public int MaxAiTokensPerMonth { get; set; } = 10000;

    public bool AllowImageGeneration { get; set; } = false;

    public bool AllowApiAccess { get; set; } = false;

    public bool AllowWorkflowAutomation { get; set; } = false;
    
    public DateTime PlanStartUtc { get; set; }
    
    public DateTime? PlanEndUtc { get; set; }

    [ForeignKey(nameof(TenantId))]
    public Tenant Tenant { get; set; }
}