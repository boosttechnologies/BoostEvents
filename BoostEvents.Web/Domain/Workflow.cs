using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class Workflow : BaseEntity
{
    [Required]
    public Guid BusinessInstanceId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    public ICollection<WorkflowStep> Steps { get; set; }
}