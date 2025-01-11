using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPRRewrite2.Modellen;

public class Bedrijf
{
    public int BedrijfId { get; set; }
    public int KvkNummer { get; set; }
    [MaxLength(255)] public string Bedrijfsnaam { get; set; }
    [MaxLength(255)] public string Domeinnaam { get; set; }

    public int BedrijfAdres { get; set; }
    [ForeignKey(nameof(BedrijfAdres))] public Adres Adres { get; set; }
    
    public Bedrijf() {}

    public Bedrijf(int kvkNummer, string bedrijfsnaam, string domeinnaam, int bedrijfAdres)
    {
        KvkNummer = kvkNummer;
        Bedrijfsnaam = bedrijfsnaam;
        Domeinnaam = domeinnaam;
        BedrijfAdres = bedrijfAdres;
    }
}