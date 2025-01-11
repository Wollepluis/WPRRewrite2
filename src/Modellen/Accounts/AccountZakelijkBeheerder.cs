using WPRRewrite2.DTOs;

namespace WPRRewrite2.Modellen.Accounts;

public class AccountZakelijkBeheerder : AccountZakelijk
{
    public AccountZakelijkBeheerder() {}
    public AccountZakelijkBeheerder(string email, string wachtwoord, int bedrijfId) {}
    
    public override void UpdateAccount(AccountDto nieuweGegevens)
    {
        Email = nieuweGegevens.Email;
        Wachtwoord = nieuweGegevens.Wachtwoord;
        BedrijfId = nieuweGegevens.BedrijfId;
    }
}