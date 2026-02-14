using System.ComponentModel.DataAnnotations;

namespace DomnerTech.CATemplate.Application.DTOs;

public record BaseRequest
{
    [Required]
    public Guid UserId { get; set; }
}