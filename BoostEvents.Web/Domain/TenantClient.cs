using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class TenantClient : BaseEntity
{
    [Required]
    public Guid BusinessInstanceId { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string LastName { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(25)]
    public string Phone { get; set; }

    [MaxLength(100)]
    public string ExternalReferenceId { get; set; } // For Stripe, Monday.com, etc.

    /// <summary>
    /// JSON blob for extensible custom fields (e.g. dietary restrictions, preferences).
    /// </summary>
    [MaxLength(2048)]
    public string MetadataJson { get; set; }
}