using System.ComponentModel.DataAnnotations;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace BoostEvents.Web.Features.Businesses.Requests;

public class UpdateBusinessRequest
{
    [Required]
    [FromRoute, BindFrom("id")]
    public Guid Id { get; set; }

    [Required, MaxLength(255)]
    public string Name { get; set; } = default!;
}