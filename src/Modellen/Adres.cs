using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace WPRRewrite2.Modellen;

public class Adres
{
    private static readonly HttpClient HttpClient = new();
    
    public int AdresId { get; set; }
    [MaxLength(255)] public string Straatnaam { get; set; }
    public int Huisnummer { get; set; }
    [MaxLength(255)] public string Postcode { get; set; }
    [MaxLength(255)] public string Woonplaats { get; set; }
    [MaxLength(255)] public string Gemeente { get; set; }
    [MaxLength(255)] public string Provincie { get; set; }
    
    public static async Task<Adres?> ZoekAdres(string postcode, int huisnummer)
    {
        if (string.IsNullOrWhiteSpace(postcode) || huisnummer <= 0)
            throw new ArgumentException("Postcode and huisnummer must be valid.");

        var apiUrl = 
            $"https://api.pdok.nl/bzk/locatieserver/search/v3_1/free?q=postcode:{postcode} AND huisnummer:{huisnummer}";

        try
        {
            var response = await HttpClient.GetAsync(apiUrl).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var json = JObject.Parse(jsonResponse);

                var adresData = json["response"]?["docs"]?[0];
                if (adresData != null)
                {
                    return new Adres
                    {
                        Straatnaam = (string?)adresData["straatnaam"] ?? "Unknown",
                        Huisnummer = (int?)adresData["huisnummer"] ?? 0,
                        Postcode = (string?)adresData["postcode"] ?? string.Empty,
                        Woonplaats = (string?)adresData["woonplaatsnaam"] ?? "Unknown",
                        Gemeente = (string?)adresData["gemeentenaam"] ?? "Unknown",
                        Provincie = (string?)adresData["provincienaam"] ?? "Unknown"
                    };
                }
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error fetching address: {ex.Message}", ex);
        }
        return null;
    }
}