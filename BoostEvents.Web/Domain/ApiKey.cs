using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class ApiKey : BaseEntity
{
    [Required]
    public Guid TenantId { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Name { get; set; } // Friendly label (e.g. "Zapier Integration")

    [Required]
    [MaxLength(128)]
    public required string HashedKey { get; set; } // Store securely, not raw
    
    /// <summary>
    /// JSON string describing the permissions or endpoints allowed for this key.
    /// Example: { "scopes": ["events.read", "clients.write"] }
    /// </summary>
    [MaxLength(1024)]
    public string? ScopeJson { get; set; }
    
    [Required]
    public Guid CreatedByUserId { get; set; }
    
    public DateTime? ExpiresUtc { get; set; }

    public bool IsRevoked { get; set; } = false;

    [MaxLength(1024)]
    public string? Notes { get; set; } // Optional metadata for devs/admins
}