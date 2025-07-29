using System.ComponentModel.DataAnnotations;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace BoostEvents.Web.Features.BusinessInstances.Requests;

public class UpdateBusinessInstanceRequest
{
    [Required]
    [FromRoute, BindFrom("id")]
    public Guid Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = default!;

    public DateTime? StartUtc { get; set; }

    public DateTime? EndUtc { get; set; }

    [Required]
    public Guid BusinessId { get; set; }
}