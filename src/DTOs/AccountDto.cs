namespace WPRRewrite2.DTOs
{
    public class AccountDto
    {
        public AccountDto(string accountType, string email, string wachtwoord, string naam, int nummer, int adresId)
        {
            AccountType = accountType;
            Email = email;
            Wachtwoord = wachtwoord;
            Naam = naam;
            Nummer = nummer;
            AdresId = adresId;
        }
        
        public string AccountType { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string Naam { get; set; }
        public int Nummer { get; set; }
        public int AdresId { get; set; }
    }
}