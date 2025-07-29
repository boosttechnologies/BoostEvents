using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class SaleItem : BaseEntity
{
    [Required]
    public Guid BusinessInstanceId { get; set; }

    [Required, MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(750)]
    public string? Description { get; set; }

    public bool IsVisibleToPublic { get; set; }

    public int? MaxQuantity { get; set; }

    public int? InventoryCount { get; set; }

    [MaxLength(150)]
    public string? Category { get; set; }

    public ICollection<SaleItemPriceTier>? PriceTiers { get; set; }

    public ICollection<DiscountCode>? DiscountCodes { get; set; }
}