using System.ComponentModel.DataAnnotations;

namespace LocoTesting.Application.Dtos.Test;

public class CreateTestDto
{
    [Required]
    [MaxLength(50)]
    public string Title { get; set; }
    
    [Required]
    [MaxLength(500)]
    public string Description { get; set; }
}