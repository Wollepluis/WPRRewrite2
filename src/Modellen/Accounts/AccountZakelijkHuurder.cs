using WPRRewrite2.DTOs;

namespace WPRRewrite2.Modellen.Accounts;

public class AccountZakelijkHuurder(string email, string wachtwoord, int bedrijfId)
    : AccountZakelijk(email, wachtwoord, bedrijfId)
{
    public override void UpdateAccount(AccountDto nieuweGegevens)
    {
        Email = nieuweGegevens.Email;
        Wachtwoord = nieuweGegevens.Wachtwoord;
    }
}