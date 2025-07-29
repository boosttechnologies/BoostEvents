using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class FormTemplate : BaseEntity
{
    [Required]
    public Guid BusinessInstanceId { get; set; }

    [Required, MaxLength(100)]
    public required string Name { get; set; }

    public ICollection<FormFieldTemplate>? Fields { get; set; }
}