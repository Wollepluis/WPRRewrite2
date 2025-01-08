using System.ComponentModel.DataAnnotations.Schema;
using WPRRewrite2.Modellen;

namespace WPRRewrite2.Interfaces;

public interface IAccountParticulier
{
    string Naam { get; set; }
    int Telefoonnummer { get; set; }
    
    int? AdresId { get; set; }
    [ForeignKey("AdresId")] Adres? Adres { get; set; }

    void UpdateAccount(IAccount account);
}