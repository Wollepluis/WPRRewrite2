using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WPRRewrite2.DTOs;
using WPRRewrite2.Interfaces;

namespace WPRRewrite2.Modellen.Abbo;

public abstract class Abonnement(int maxVoertuigen, int maxWerknemers, DateOnly begindatum, string abonnementType, 
    int bedrijfId) : IAbonnement
{
    public int AbonnementId { get; set; }
    public int MaxVoertuigen { get; set; } = maxVoertuigen;
    public int MaxWerknemers { get; set; } = maxWerknemers;
    public DateOnly BeginDatum { get; set; } = begindatum;
    
    [MaxLength(255)]public string AbonnementType { get; set; } = abonnementType;

    public int BedrijfId { get; set; } = bedrijfId;
    [ForeignKey(nameof(BedrijfId))] public Bedrijf Bedrijf { get; set; }
    
    public static Abonnement MaakAbonnement(AbonnementDto gegevens)
    {
        Abonnement nieuwAbonnement;
        switch (gegevens.AbonnementType)
        {
            case "UpFront":
                nieuwAbonnement = new UpFront(gegevens.MaxVoertuigen, gegevens.MaxWerknemers, gegevens.BeginDatum,
                    gegevens.AbonnementType, gegevens.BedrijfId);
                break;
            case "PayAsYouGo":
                nieuwAbonnement = new PayAsYouGo(gegevens.MaxVoertuigen, gegevens.MaxWerknemers, gegevens.BeginDatum,
                    gegevens.AbonnementType, gegevens.BedrijfId);
                break;
            default:
                throw new ArgumentException($"Onbekend account type: {gegevens.AbonnementType}");
        }
        return nieuwAbonnement;
    }
}