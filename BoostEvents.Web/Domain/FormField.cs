using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoostEvents.Web.Domain;

public class FormField : BaseEntity
{
    [Required]
    public Guid WorkflowStepId { get; set; }

    [Required, MaxLength(100)]
    public string Label { get; set; }
    
    [Required]
    public FormFieldType FieldType { get; set; }

    public string? OptionsJson { get; set; }

    [ForeignKey(nameof(WorkflowStepId))]
    public WorkflowStep Step { get; set; }

    public FormFieldTemplate? Template { get; set; }
}