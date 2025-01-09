using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using WPRRewrite2.Interfaces;

namespace WPRRewrite2.Modellen.Kar;

public class Voertuig : IVoertuig
{
    public int VoertuigId { get; set; }
    [MaxLength(255)] public string Kenteken { get; set; }
    [MaxLength(255)] public string Merk { get; set; }
    [MaxLength(255)] public string Model { get; set; }
    [MaxLength(255)] public string Kleur { get; set; }
    public int Aanschafjaar { get; set; }
    public int Prijs { get; set; }
    [MaxLength(255)] public string VoertuigStatus { get; set; }
    [MaxLength(255)] public string BrandstofType { get; set; }
    public List<Reservering> Reserveringen { get; set; }

    public Voertuig(string kenteken, string merk, string model, string kleur, int aanschafjaar, int prijs,
        string brandstofType)
    {
        Kenteken = kenteken;
        Merk = merk;
        Model = model;
        Kleur = kleur;
        Aanschafjaar = aanschafjaar;
        Prijs = prijs;
        VoertuigStatus = "Beschikbaar";
        BrandstofType = brandstofType;
    }

    public void UpdateVoertuig(IVoertuig voertuig)
    {
        Kenteken = voertuig.Kenteken;
        Merk = voertuig.Merk;
        Model = voertuig.Model;
        Kleur = voertuig.Kleur;
        Aanschafjaar = voertuig.Aanschafjaar;
        Prijs = voertuig.Prijs;
    }

    public void UpdateVoertuigStatus(string status)
    {
        VoertuigStatus = status;
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