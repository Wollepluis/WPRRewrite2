using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace WPRRewrite2.Modellen;

public class Adres
{
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
        {
            throw new ArgumentException("Postcode and huisnummer must be valid.");
        }

        string apiUrl =
            $"https://api.pdok.nl/bzk/locatieserver/search/v3_1/free?q=postcode:{postcode} AND huisnummer:{huisnummer}";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Make HTTP GET call
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Parse JSON response
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(jsonResponse);

                    var adresData = json["response"]?["docs"]?[0]; // First result
                    if (adresData != null)
                    {
                        // Map JSON response to Adres object
                        return new Adres()
                        {
                            Straatnaam = (string?)adresData["straatnaam"],
                            Huisnummer = (int?)adresData["huisnummer"] ?? 0,
                            Postcode = (string?)adresData["postcode"],
                            Woonplaats = (string?)adresData["woonplaatsnaam"],
                            Gemeente = (string?)adresData["gemeentenaam"],
                            Provincie = (string?)adresData["provincienaam"]
                        };
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error fetching address: {ex.Message}", ex);
        }

        return null;
    }
    
    //Methode Registratiecheck
}