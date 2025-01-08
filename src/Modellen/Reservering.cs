using System.ComponentModel.DataAnnotations.Schema;
using WPRRewrite2.Modellen.Kar;
    
namespace WPRRewrite2.Modellen;

public class Reservering
{
    public int ReserveringId { get; set; }
    public DateOnly Begindatum { get; set; }
    public DateOnly Einddatum { get; set; }

    public int VoertuigId { get; set; }
    [ForeignKey(nameof(VoertuigId))] public Voertuig Voertuig { get; set; }

    public bool IsGoedgekeurd { get; set; }

    public double TotaalPrijs { get; set; }
    public bool IsBetaald { get; set; }
}