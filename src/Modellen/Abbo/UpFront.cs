namespace WPRRewrite2.Modellen.Abbo;

public class UpFront : Abonnement
{
    public UpFront() {}
    public UpFront(int maxVoertuigen, int maxMedewerkers, DateOnly begindatum, string abonnementType, int bedrijfId)
        : base(maxVoertuigen, maxMedewerkers, begindatum, abonnementType, bedrijfId)
    {}
}