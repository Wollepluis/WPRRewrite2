using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WPRRewrite2.DTOs;
using WPRRewrite2.Interfaces;

namespace WPRRewrite2.Modellen.Kar;

public class Voertuig(string kenteken, string merk, string model, string kleur, int aanschafjaar, int prijs,
    string brandstofType) : IVoertuig
{
    public int VoertuigId { get; set; }
    [MaxLength(255)] public string Kenteken { get; set; } = kenteken;
    [MaxLength(255)] public string Merk { get; set; } = merk;
    [MaxLength(255)] public string Model { get; set; } = model;
    [MaxLength(255)] public string Kleur { get; set; } = kleur;
    public int Aanschafjaar { get; set; } = aanschafjaar;
    public int Prijs { get; set; } = prijs;
    [MaxLength(255)] public string VoertuigStatus { get; set; } = "Beschikbaar";
    [MaxLength(255)] public string BrandstofType { get; set; } = brandstofType;

    public int? ReserveringId { get; set; }
    [ForeignKey(nameof(ReserveringId))] public Reservering Reservering { get; set; }

    public void UpdateVoertuig(IVoertuig voertuig)
    {
        Kenteken = voertuig.Kenteken;
        Merk = voertuig.Merk;
        Model = voertuig.Model;
        Kleur = voertuig.Kleur;
        Aanschafjaar = voertuig.Aanschafjaar;
        Prijs = voertuig.Prijs;
        VoertuigStatus = voertuig.VoertuigStatus;
    }

    public static Voertuig MaakVoertuig(VoertuigDto gegevens)
    {
        Voertuig nieuwVoertuig;
        switch (gegevens.VoertuigType)
        {
            case "Auto":
                nieuwVoertuig = new Auto(gegevens.Kenteken, gegevens.Merk, gegevens.Model, gegevens.Kleur,
                    gegevens.Aanschafjaar, gegevens.Prijs, gegevens.BrandstofType);
                break;
            case "Camper":
                nieuwVoertuig = new Camper(gegevens.Kenteken, gegevens.Merk, gegevens.Model, gegevens.Kleur,
                    gegevens.Aanschafjaar, gegevens.Prijs, gegevens.BrandstofType);
                break;
            case "Caravan":
                nieuwVoertuig = new Caravan(gegevens.Kenteken, gegevens.Merk, gegevens.Model, gegevens.Kleur,
                    gegevens.Aanschafjaar, gegevens.Prijs, gegevens.BrandstofType);
                break;
            default:
                throw new ArgumentException($"Onbekend account type: {gegevens.VoertuigType}");
        }

        return nieuwVoertuig;
    }
    
    // public int BerekenKosten()
    // {
    //     var days = (voertuigReservering.Einddatum - voertuigReservering.Begindatum).Days;
    //     var bijkomendeKosten = 0;
    //     if (this.typeof(Auto))
    //     {
    //         bijkomendeKosten = 100 + 100 * days; 
    //     } else if (voertuig.VoertuigType == "Caravan")
    //     {
    //         bijkomendeKosten = 200 + 200 * days; 
    //     } else if (voertuig.VoertuigType == "Camper")
    //     {
    //         bijkomendeKosten = 300 + 300 * days;
    //     }
    // }
}