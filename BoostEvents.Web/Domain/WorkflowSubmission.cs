using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class WorkflowSubmission : BaseEntity
{
    [Required]
    public Guid WorkflowId { get; set; }

    [Required]
    public Guid BusinessInstanceId { get; set; }

    /// <summary>
    /// Optionally tied to a client (e.g., attendee).
    /// </summary>
    public Guid? ClientId { get; set; }

    /// <summary>
    /// User or system who initiated the submission (could be admin).
    /// </summary>
    public Guid? SubmittedByUserId { get; set; }
    
    public DateTime SubmittedUtc { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Optional status: "complete", "pending", "approved", etc.
    /// </summary>
    [MaxLength(50)]
    public string Status { get; set; }

    /// <summary>
    /// Optional notes, reasons, or context about the submission.
    /// </summary>
    [MaxLength(1024)]
    public string Notes { get; set; }

    /// <summary>
    /// Answers to individual steps captured as part of this submission.
    /// </summary>
    public List<WorkflowStepAnswer> StepAnswers { get; set; }
}