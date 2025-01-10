namespace WPRRewrite2.Modellen.Abbo;

public class UpFront(int maxVoertuigen, int maxWerknemers, DateOnly begindatum, string abonnementType, int bedrijfId) 
    : Abonnement(maxVoertuigen, maxWerknemers, begindatum, abonnementType, bedrijfId);