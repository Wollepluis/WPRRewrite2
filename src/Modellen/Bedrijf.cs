using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPRRewrite2.Modellen;

public class Bedrijf(int kvkNummer, string bedrijfsnaam, string domeinnaam, int adresId)
{
    public int BedrijfId { get; set; }
    public int KvkNummer { get; set; } = kvkNummer;
    [MaxLength(255)] public string Bedrijfsnaam { get; set; } = bedrijfsnaam;
    [MaxLength(255)] public string Domeinnaam { get; set; } = domeinnaam;

    public int AdresId { get; set; } = adresId;
    [ForeignKey(nameof(AdresId))] public Adres Adres { get; set; }
}