namespace WPRRewrite2.Modellen.Account;

public abstract class AccountMedewerker : Account
{
    protected AccountMedewerker(string email, string wachtwoord) 
        : base(email, wachtwoord)
    {
        
    }
}