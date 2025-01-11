namespace WPRRewrite2.Modellen.Abbo;

public class UpFront : Abonnement
{
    public UpFront() {}
    public UpFront(int maxVoertuigen, int maxWerknemers, DateOnly begindatum, string abonnementType, int bedrijfId)
        : base(maxVoertuigen, maxWerknemers, begindatum, abonnementType, bedrijfId)
    {}
}