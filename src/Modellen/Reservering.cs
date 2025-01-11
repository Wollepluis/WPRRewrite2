using System.ComponentModel.DataAnnotations.Schema;
using WPRRewrite2.Modellen.Accounts;
using WPRRewrite2.Modellen.Kar;
    
namespace WPRRewrite2.Modellen;

public class Reservering
{
    public int ReserveringId { get; set; }
    public DateOnly Begindatum { get; set; }
    public DateOnly Einddatum { get; set; }

    public int AccountId { get; set; }
    [ForeignKey(nameof(AccountId))] public Account Account;
    
    public int VoertuigId { get; set; }
    [ForeignKey(nameof(VoertuigId))] public Voertuig Voertuig { get; set; }

    public bool IsGoedgekeurd { get; set; }

    public double TotaalPrijs { get; set; }
    public bool IsBetaald { get; set; }
    
    public Reservering() {}
    public Reservering(DateOnly begindatum, DateOnly einddatum, int accountId, int voertuigId, bool isGoedgekeurd,
        double totaalPrijs, bool isBetaald)
    {
        Begindatum = begindatum;
        Einddatum = einddatum;
        AccountId = accountId;
        VoertuigId = voertuigId;
        IsGoedgekeurd = isGoedgekeurd;
        TotaalPrijs = totaalPrijs;
        IsBetaald = isBetaald;
    }
}