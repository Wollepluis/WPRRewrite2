namespace WPRRewrite2.Modellen.Abbo;

public class PayAsYouGo : Abonnement
{
    public PayAsYouGo() {}
    public PayAsYouGo(int maxVoertuigen, int maxWerknemers, DateOnly begindatum, string abonnementType, int bedrijfId)
        : base(maxVoertuigen, maxWerknemers, begindatum, abonnementType, bedrijfId)
    {}
}