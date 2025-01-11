using System.ComponentModel.DataAnnotations.Schema;
using WPRRewrite2.Modellen;

namespace WPRRewrite2.Interfaces;

public interface IAbonnement
{
    int AbonnementId { get; set; }
    int MaxVoertuigen { get; set; }
    int MaxMedewerkers { get; set; }
    string AbonnementType { get; set; }
    DateOnly BeginDatum { get; set; }
    
    int BedrijfId { get; set; }
    [ForeignKey(nameof(BedrijfId))] public Bedrijf Bedrijf { get; set; }
    
    
}