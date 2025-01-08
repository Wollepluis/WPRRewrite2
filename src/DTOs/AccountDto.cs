namespace WPRRewrite2.DTOs;

public class AccountDto(string accountType, string email, string wachtwoord, string naam, int telefoonnummer, 
    int bedrijfId, int adresId)
{
    public string AccountType { get; set; } = accountType;
    public string Email { get; set; } = email;
    public string Wachtwoord { get; set; } = wachtwoord;
    public string Naam { get; set; } = naam;
    public int Telefoonnummer { get; set; } = telefoonnummer;

    public int BedrijfId { get; set; } = bedrijfId;
    public int AdresId { get; set; } = adresId;




}