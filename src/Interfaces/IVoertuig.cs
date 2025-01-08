using WPRRewrite2.Modellen;

namespace WPRRewrite2.Interfaces;

public interface IVoertuig
{
    int VoertuigId { get; set; }
    string Kenteken { get; set; }
    string Merk { get; set; }
    string Model { get; set; }
    string Kleur { get; set; }
    int Aanschafjaar { get; set; }
    int Prijs { get; set; }
    string VoertuigStatus { get; set; }
    string BrandstofType { get; set; }
    List<Reservering> Reserveringen { get; set; }

    public void UpdateVoertuig(IVoertuig updatedVoertuig);
}