using System.ComponentModel.DataAnnotations.Schema;
using WPRRewrite2.DTOs;
using WPRRewrite2.Interfaces;

namespace WPRRewrite2.Modellen.Account;

public class AccountParticulier : Account
{
    public string Naam { get; set; }
    public int Telefoonnummer { get; set; }
    
    public int AdresId { get; set; }
    [ForeignKey(nameof(AdresId))] public Adres Adres { get; set; }


    public AccountParticulier(string email, string wachtwoord, string naam, int telefoonnummer, int adresId) 
        : base(email, wachtwoord)
    {
        Naam = naam;
        Telefoonnummer = telefoonnummer;
        AdresId = adresId;
    }

    public override void UpdateAccount(AccountDto nieuweGegevens)
    {
        Email = nieuweGegevens.Email;
        Wachtwoord = nieuweGegevens.Wachtwoord;
        Naam = nieuweGegevens.Naam;
        Telefoonnummer = nieuweGegevens.Telefoonnummer;
    }
}