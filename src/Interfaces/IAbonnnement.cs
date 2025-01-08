using WPRRewrite2.Modellen.Abbo;

namespace WPRRewrite2.Interfaces;

public interface IAbonnement
{
    int AbonnementId { get; set; }
    int MaxVoertuigen { get; set; }
    int MaxWerknemers { get; set; }
    DateOnly BeginDatum { get; set; }
    
}