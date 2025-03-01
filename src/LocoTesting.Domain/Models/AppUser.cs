using Microsoft.AspNetCore.Identity;

namespace LocoTesting.Domain.Models;

public class AppUser : IdentityUser
{
    public int UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; }
}