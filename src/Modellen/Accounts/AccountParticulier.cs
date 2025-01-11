﻿using System.ComponentModel.DataAnnotations.Schema;
using WPRRewrite2.DTOs;

namespace WPRRewrite2.Modellen.Accounts;

public class AccountParticulier : Account
{
    public string Naam { get; set; }
    public int Telefoonnummer { get; set; }

    public int AdresId { get; set; }
    [ForeignKey(nameof(AdresId))] public Adres Adres { get; set; }

    public AccountParticulier() {}
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