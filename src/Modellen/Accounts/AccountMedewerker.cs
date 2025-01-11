namespace WPRRewrite2.Modellen.Accounts;

public abstract class AccountMedewerker : Account
{
    protected AccountMedewerker() {}
    protected AccountMedewerker(string email, string wachtwoord) 
        : base(email, wachtwoord)
    {}
}