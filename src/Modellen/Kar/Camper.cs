namespace WPRRewrite2.Modellen.Kar;

public class Camper : Voertuig
{
    public Camper(string kenteken, string merk, string model, string kleur, int aanschafjaar, int prijs,
        string brandstofType)
        : base(kenteken, merk, model, kleur, aanschafjaar, prijs, brandstofType)
    {
    }
}