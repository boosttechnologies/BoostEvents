using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class WorkflowStepRule : BaseEntity
{
    [Required]
    public Guid WorkflowStepId { get; set; }

    /// <summary>
    /// JSON-encoded logic defining the condition that triggers this rule.
    /// Example: { "fieldId": "isVIP", "equals": true }
    /// </summary>
    [Required]
    [MaxLength(2048)]
    public string ConditionJson { get; set; }

    /// <summary>
    /// JSON describing the action to take if condition is met.
    /// Example: { "action": "SendWebhook", "url": "https://..." }
    /// </summary>
    [Required]
    [MaxLength(2048)]
    public string ActionJson { get; set; }
}