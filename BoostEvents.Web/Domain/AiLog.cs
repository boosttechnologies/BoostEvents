using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class AiLog : BaseEntity
{
    [Required]
    public Guid BusinessId { get; set; }

    /// <summary>
    /// The prompt or command sent to the AI system.
    /// </summary>
    [Required]
    [MaxLength(2048)]
    public required string Prompt { get; set; }

    /// <summary>
    /// The number of AI tokens used for this interaction (text or image).
    /// </summary>
    [Required]
    public int TokensUsed { get; set; }

    /// <summary>
    /// The raw result or a summary of what was returned (can be truncated).
    /// </summary>
    [MaxLength(2048)]
    public string? ResultPreview { get; set; }
    
}