using WPRRewrite2.DTOs;
using WPRRewrite2.Interfaces;

namespace WPRRewrite2.Modellen.Account;

public class AccountMedewerkerFrontoffice : AccountMedewerker
{
    public AccountMedewerkerFrontoffice(string email, string wachtwoord) 
        : base(email, wachtwoord)
    {
    }

    public override void UpdateAccount(AccountDto nieuweGegevens)
    {
        Email = nieuweGegevens.Email;
        Wachtwoord = nieuweGegevens.Wachtwoord;
    }
}