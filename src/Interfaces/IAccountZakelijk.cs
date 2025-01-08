using System.ComponentModel.DataAnnotations.Schema;
using WPRRewrite2.Modellen;

namespace WPRRewrite2.Interfaces;

public interface IAccountZakelijk
{
    int BedrijfId { get; set; }

    [ForeignKey("BedrijfId")] Bedrijf Bedrijf { get; set; }
}