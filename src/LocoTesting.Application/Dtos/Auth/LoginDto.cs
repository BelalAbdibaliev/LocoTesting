using System.ComponentModel.DataAnnotations;

namespace LocoTesting.Application.Dtos.Auth;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}