using WPRRewrite2.DTOs;

namespace WPRRewrite2.Modellen.Account;

public class AccountZakelijkBeheerder : AccountZakelijk
{
    public AccountZakelijkBeheerder(string email, string wachtwoord, int bedrijfId) 
        : base(email, wachtwoord, bedrijfId)
    {
    }

    public override void UpdateAccount(AccountDto nieuweGegevens)
    {
        Email = nieuweGegevens.Email;
        Wachtwoord = nieuweGegevens.Wachtwoord;
        BedrijfId = nieuweGegevens.BedrijfId;
    }
}