using WPRRewrite2.DTOs;

namespace WPRRewrite2.Modellen.Accounts;

public class AccountZakelijkBeheerder(string email, string wachtwoord, int bedrijfId)
    : AccountZakelijk(email, wachtwoord, bedrijfId)
{
    public override void UpdateAccount(AccountDto nieuweGegevens)
    {
        Email = nieuweGegevens.Email;
        Wachtwoord = nieuweGegevens.Wachtwoord;
        BedrijfId = nieuweGegevens.BedrijfId;
    }
}