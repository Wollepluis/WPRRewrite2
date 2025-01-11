using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPRRewrite2.Modellen;

public class Bedrijf
{
    public int BedrijfId { get; set; }
    public int KvkNummer { get; set; }
    [MaxLength(255)] public string Bedrijfsnaam { get; set; }
    [MaxLength(255)] public string Domeinnaam { get; set; }

    public int AdresId { get; set; }
    [ForeignKey(nameof(AdresId))] public Adres Adres { get; set; }
    
    public Bedrijf() {}

    public Bedrijf(int kvkNummer, string bedrijfsnaam, string domeinnaam, int adresId)
    {
        KvkNummer = kvkNummer;
        Bedrijfsnaam = bedrijfsnaam;
        Domeinnaam = domeinnaam;
        AdresId = adresId;
    }
}