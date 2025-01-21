namespace LocoTesting.Application.Dtos.Auth;

public class AuthResponseDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Token { get; set; }
}