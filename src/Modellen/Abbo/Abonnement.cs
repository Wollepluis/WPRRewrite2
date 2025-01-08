using WPRRewrite2.Interfaces;

namespace WPRRewrite2.Modellen.Abbo;

public class Abonnement : IAbonnement
{
    public int AbonnementId { get; set; }
    public int MaxVoertuigen { get; set; }
    public int MaxWerknemers { get; set; }
    public DateOnly BeginDatum { get; set; }

    public Abonnement()
    {
    }

    public Abonnement(int maxVoertuigen, int maxWerknemers, DateOnly beginDatum)
    {
        MaxVoertuigen = maxVoertuigen;
        MaxWerknemers = maxWerknemers;
        BeginDatum = beginDatum;
    }
}