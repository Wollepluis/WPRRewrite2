using System.ComponentModel.DataAnnotations.Schema;
using WPRRewrite2.Interfaces;

namespace WPRRewrite2.Modellen.Accounts;

public abstract class AccountZakelijk : Account, IAccountZakelijk
{
    public int BedrijfId { get; set; }
    [ForeignKey(nameof(BedrijfId))] public Bedrijf Bedrijf { get; set; }

    protected AccountZakelijk(string email, string wachtwoord, int bedrijfId) 
        : base(email, wachtwoord)
    {
        BedrijfId = bedrijfId;
    }
}