using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class FormFieldTemplate : BaseEntity
{
    [Required]
    public Guid FormTemplateId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string Label { get; set; }
    
    [MaxLength(1000)]
    public string? HelpText { get; set; }

    [Required]
    public FormFieldType FieldType { get; set; }

    /// <summary>
    /// Optional pre-filled dropdown options, etc.
    /// </summary>
    [MaxLength(2048)]
    public string? OptionsJson { get; set; }
}