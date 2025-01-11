namespace WPRRewrite2.Modellen.Abbo;

public class PayAsYouGo : Abonnement
{
    public PayAsYouGo() {}
    public PayAsYouGo(int maxVoertuigen, int maxMedewerkers, DateOnly begindatum, string abonnementType, int bedrijfId)
        : base(maxVoertuigen, maxMedewerkers, begindatum, abonnementType, bedrijfId)
    {}
}