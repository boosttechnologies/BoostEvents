using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class Tenant : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    /// <summary>
    /// A unique code or slug for URL routing or integration.
    /// </summary>
    [MaxLength(50)]
    public string? Slug { get; set; }

    /// <summary>
    /// Indicates whether this tenant is active (useful for soft-deactivation).
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation Properties (use only if needed in EF navigation)
    public List<Business> Businesses { get; set; }
    public List<TenantPlan> Plans { get; set; }
    public List<ApiKey> ApiKeys { get; set; }
    public List<TenantAiUsage> AiUsages { get; set; }
}