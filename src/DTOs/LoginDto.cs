namespace WPRRewrite2.DTOs;

public class LoginDto(string email, string wachtwoord)
{
    public string Email { get; } = email;
    public string Wachtwoord { get; } = wachtwoord;
}