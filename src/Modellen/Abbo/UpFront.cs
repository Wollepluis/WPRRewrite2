namespace WPRRewrite2.Modellen.Abbo;

public class UpFront : Abonnement
{
    public UpFront()
    {
    }
    
    public UpFront(int maxVoertuigen, int maxWerknemers, DateOnly beginDatum)
    {
        MaxVoertuigen = maxVoertuigen;
        MaxWerknemers = maxWerknemers;
        BeginDatum = beginDatum;
    }
}