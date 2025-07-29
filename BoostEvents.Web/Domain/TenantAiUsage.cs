using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class TenantAiUsage : BaseEntity
{
    [Required]
    public Guid TenantId { get; set; }

    /// <summary>
    /// Start of the metering period (inclusive).
    /// </summary>
    [Required]
    public DateTime PeriodStartUtc { get; set; }

    /// <summary>
    /// End of the metering period (exclusive).
    /// </summary>
    [Required]
    public DateTime PeriodEndUtc { get; set; }

    /// <summary>
    /// Total tokens used across all AI prompts.
    /// </summary>
    public int TokensUsed { get; set; }

    /// <summary>
    /// Total image generations (if applicable).
    /// </summary>
    public int ImagesGenerated { get; set; }

    /// <summary>
    /// Optional JSON for breakdown by feature (e.g. { "imageTool": 30, "summarizer": 400 }).
    /// </summary>
    [MaxLength(2048)]
    public string FeatureBreakdownJson { get; set; }
}