namespace WPRRewrite2.Modellen.Abbo;

public class PayAsYouGo : Abonnement
{
    public PayAsYouGo()
    {
    }

    public PayAsYouGo(int maxVoertuigen, int maxWerknemers, DateOnly beginDatum)
    {
        MaxVoertuigen = maxVoertuigen;
        MaxWerknemers = maxWerknemers;
        BeginDatum = beginDatum;
    }
}