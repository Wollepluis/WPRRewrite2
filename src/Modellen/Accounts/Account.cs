using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using WPRRewrite2.DTOs;
using WPRRewrite2.Interfaces;

namespace WPRRewrite2.Modellen.Accounts;

public abstract class Account : IAccount
{
    protected readonly IPasswordHasher<Account> PasswordHasher = new PasswordHasher<Account>();
    
    public int AccountId { get; set; }
    [MaxLength(255)] public string Email { get; set; }
    [MaxLength(255)] public string Wachtwoord { get; set; }
    
    public int ReserveringId { get; set; }
    [ForeignKey(nameof(ReserveringId))] private Reservering Reservering { get; set; }

    protected Account(string email, string wachtwoord)
    {
        Email = email;
        Wachtwoord = PasswordHasher.HashPassword(this, wachtwoord);
    }
    
    public abstract void UpdateAccount(AccountDto nieuweGegevens);

    public PasswordVerificationResult VerifyPassword(string wachtwoord)
    {
        return PasswordHasher.VerifyHashedPassword(this, Wachtwoord, wachtwoord);
    }

    public static Account MaakAccount(AccountDto gegevens)
    {
        Account nieuwAccount;
        switch (gegevens.AccountType)
        {
            case "Particulier":
                nieuwAccount = new AccountParticulier(gegevens.Email, gegevens.Wachtwoord, gegevens.Naam, 
                    gegevens.Telefoonnummer, gegevens.AdresId);
                break;
            case "ZakelijkBeheerder":
                nieuwAccount = new AccountZakelijkBeheerder(gegevens.Email, gegevens.Wachtwoord, gegevens.BedrijfId);
                break;
            case "ZakelijkHuurder":
                nieuwAccount = new AccountZakelijkHuurder(gegevens.Email, gegevens.Wachtwoord, gegevens.BedrijfId);
                break;
            case "Frontoffice":
                nieuwAccount =
                    new AccountMedewerkerFrontoffice(gegevens.Email, gegevens.Wachtwoord);
                break;
            case "Backoffice":
                nieuwAccount = new AccountMedewerkerBackoffice(gegevens.Email, gegevens.Wachtwoord);
                break;
            default:
                throw new ArgumentException($"Onbekend account type: {gegevens.AccountType}");
        }

        return nieuwAccount;
    }
}