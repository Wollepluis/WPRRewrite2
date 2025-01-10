using System.ComponentModel.DataAnnotations.Schema;
using WPRRewrite2.DTOs;

namespace WPRRewrite2.Modellen.Accounts;

public class AccountParticulier(string email, string wachtwoord, string naam, int telefoonnummer, int adresId)
    : Account(email, wachtwoord)
{
    public string Naam { get; set; } = naam;
    public int Telefoonnummer { get; set; } = telefoonnummer;

    public int AdresId { get; set; } = adresId;
    [ForeignKey(nameof(AdresId))] public Adres Adres { get; set; }


    public override void UpdateAccount(AccountDto nieuweGegevens)
    {
        Email = nieuweGegevens.Email;
        Wachtwoord = nieuweGegevens.Wachtwoord;
        Naam = nieuweGegevens.Naam;
        Telefoonnummer = nieuweGegevens.Telefoonnummer;
    }
}