using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WPRRewrite2.DTOs;
using WPRRewrite2.Interfaces;

namespace WPRRewrite2.Modellen.Abbo;

public abstract class Abonnement : IAbonnement
{
    public int AbonnementId { get; set; }
    public int MaxVoertuigen { get; set; }
    public int MaxMedewerkers { get; set; }
    public DateOnly BeginDatum { get; set; }
    
    [MaxLength(255)]public string AbonnementType { get; set; }

    public int BedrijfId { get; set; }
    [ForeignKey(nameof(BedrijfId))] public Bedrijf Bedrijf { get; set; }

    protected Abonnement() {}
    protected Abonnement(int maxVoertuigen, int maxMedewerkers, DateOnly begindatum, string abonnementType, int bedrijfId)
    {
        MaxVoertuigen = maxVoertuigen;
        MaxMedewerkers = maxMedewerkers;
        BeginDatum = begindatum;
        AbonnementType = abonnementType;
        BedrijfId = bedrijfId;
    }
    
    public static Abonnement MaakAbonnement(AbonnementDto gegevens)
    {
        return gegevens.AbonnementType switch
        {
            "UpFront" => new UpFront(
                gegevens.MaxVoertuigen, 
                gegevens.MaxMedewerkers, 
                gegevens.BeginDatum,
                gegevens.AbonnementType, 
                gegevens.BedrijfId
            ),
            "PayAsYouGo" => new PayAsYouGo(
                gegevens.MaxVoertuigen, 
                gegevens.MaxMedewerkers, 
                gegevens.BeginDatum,
                gegevens.AbonnementType, 
                gegevens.BedrijfId
            ),
            _ => throw new ArgumentException($"Onbekend account type: {gegevens.AbonnementType}")
        };
    }
}