using System.ComponentModel.DataAnnotations;

namespace LocoTesting.Application.Dtos.Auth;

public class GoogleAuthDto
{
    [Required]
    public string GoogleToken { get; set; }
    
    [Required]
    [MaxLength(15)]
    public string UniqueUserName { get; set; }
    
    [Required]
    [MaxLength(15)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(15)]
    public string LastName { get; set; }
}