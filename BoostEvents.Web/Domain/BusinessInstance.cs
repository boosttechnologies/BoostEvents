using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class BusinessInstance : BaseEntity
{
    [Required]
    public Guid BusinessId { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; }
    
    public DateTime StartUtc { get; set; }
    
    public DateTime EndUtc { get; set; }

    public ICollection<SaleItem>? SaleItems { get; set; }

    public ICollection<Workflow>? Workflows { get; set; }

    public ICollection<DeviceToken>? Devices { get; set; }

    public ICollection<TenantClient>? Clients { get; set; }

    public ICollection<FormTemplate>? FormTemplates { get; set; }
}