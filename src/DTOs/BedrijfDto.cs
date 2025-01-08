namespace WPRRewrite2.DTOs;

public class BedrijfDto(int kvkNummer, string bedrijfsnaam, string domeinNaam, int adresId)
{
    public int KvkNummer { get; set; } = kvkNummer;
    public string Bedrijfsnaam { get; set; } = bedrijfsnaam;
    public string Domeinnaam { get; set; } = domeinNaam;
    public int AdresId { get; set; } = adresId;
}