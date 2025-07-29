using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoostEvents.Web.Domain;

public class SaleItemPriceTier : BaseEntity
{
    [Required]
    public Guid SaleItemId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    public DateTime StartUtc { get; set; }
    
    public DateTime EndUtc { get; set; }

    [ForeignKey(nameof(SaleItemId))]
    public SaleItem SaleItem { get; set; }
}