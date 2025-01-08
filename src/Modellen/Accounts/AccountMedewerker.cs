namespace WPRRewrite2.Modellen.Accounts;

public abstract class AccountMedewerker : Account
{
    protected AccountMedewerker(string email, string wachtwoord) 
        : base(email, wachtwoord)
    {
        
    }
}