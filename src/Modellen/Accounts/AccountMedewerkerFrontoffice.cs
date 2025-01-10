using WPRRewrite2.DTOs;

namespace WPRRewrite2.Modellen.Accounts;

public class AccountMedewerkerFrontoffice(string email, string wachtwoord) : AccountMedewerker(email, wachtwoord)
{
    public override void UpdateAccount(AccountDto nieuweGegevens)
    {
        Email = nieuweGegevens.Email;
        Wachtwoord = nieuweGegevens.Wachtwoord;
    }
}