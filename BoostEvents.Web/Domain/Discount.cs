using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoostEvents.Web.Domain;

public class Discount : BaseEntity
{
    [Required]
    public Guid BusinessInstanceId { get; set; }

    [MaxLength(255)]
    public required string Name { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal AmountOff { get; set; }

    public bool IsPercentage { get; set; }

    public bool IsStackable { get; set; }

    public ICollection<DiscountCode>? DiscountCodes { get; set; }
}