using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class WorkflowStepAnswer : BaseEntity
{
    [Required]
    public Guid WorkflowSubmissionId { get; set; }

    [Required]
    public Guid WorkflowStepId { get; set; }

    /// <summary>
    /// The specific FormField that was answered (if applicable).
    /// </summary>
    public Guid? FormFieldId { get; set; }

    /// <summary>
    /// Raw answer value as string, JSON, or encoded structure.
    /// </summary>
    [MaxLength(2048)]
    public string AnswerValue { get; set; }

    /// <summary>
    /// UTC timestamp of when the answer was submitted.
    /// </summary>
    public DateTime AnsweredUtc { get; set; } = DateTime.UtcNow;
}