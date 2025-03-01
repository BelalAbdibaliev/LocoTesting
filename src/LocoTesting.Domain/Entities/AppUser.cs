using Microsoft.AspNetCore.Identity;

namespace LocoTesting.Domain.Entities;

public class AppUser : IdentityUser
{
    public int UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; }
}