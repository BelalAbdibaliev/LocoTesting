using System.ComponentModel.DataAnnotations;

namespace LocoTesting.Application.Dtos.Test;

public class CreateTestDto
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set; }
}