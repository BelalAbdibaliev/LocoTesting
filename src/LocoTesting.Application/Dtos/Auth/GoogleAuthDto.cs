using System.ComponentModel.DataAnnotations;

namespace LocoTesting.Application.Dtos.Auth;

public class GoogleAuthDto
{
    [Required]
    public string GoogleToken { get; set; }
    
    [Required]
    public string UniqueUserName { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
}