using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoostEvents.Web.Domain;

public class DiscountCode : BaseEntity
{
    [Required]
    public Guid SaleItemId { get; set; }

    [Required]
    public Guid DiscountId { get; set; }

    [Required, MaxLength(100)]
    public required string Code { get; set; }

    public bool IsSingleUse { get; set; }

    public int? MaxUses { get; set; }

    [ForeignKey(nameof(SaleItemId))]
    public required SaleItem SaleItem { get; set; }

    [ForeignKey(nameof(DiscountId))]
    public required Discount Discount { get; set; }
}