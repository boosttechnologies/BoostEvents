using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class WorkflowStep : BaseEntity
{
    [Required]
    public Guid WorkflowId { get; set; }

    [Required, MaxLength(100)]
    public string Label { get; set; }

    public int Order { get; set; }

    public ICollection<FormField> Fields { get; set; }

    public ICollection<WorkflowStepRule> Rules { get; set; }
}