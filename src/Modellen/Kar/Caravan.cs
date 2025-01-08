namespace WPRRewrite2.Modellen.Kar;

public class Caravan : Voertuig
{
    public Caravan(string kenteken, string merk, string model, string kleur, int aanschafjaar, int prijs,
        string brandstofType)
        : base(kenteken, merk, model, kleur, aanschafjaar, prijs, brandstofType)
    {
    }
}