namespace LocoTesting.Domain.Entities;

public class UserProfile
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ProfilePicUrl { get; set; } = string.Empty;
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}