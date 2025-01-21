using Microsoft.AspNetCore.Identity;

namespace LocoTesting.Domain.Models;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = "Unknown";
    public string LastName { get; set; } = "Unknown";
}