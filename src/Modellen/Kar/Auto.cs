namespace WPRRewrite2.Modellen.Kar;

public class Auto : Voertuig
{
    public Auto(string kenteken, string merk, string model, string kleur, int aanschafjaar, int prijs,
        string brandstofType)
        : base(kenteken, merk, model, kleur, aanschafjaar, prijs, brandstofType)
    {
    }
}