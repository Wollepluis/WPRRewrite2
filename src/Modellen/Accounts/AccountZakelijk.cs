using System.ComponentModel.DataAnnotations.Schema;
using WPRRewrite2.Interfaces;

namespace WPRRewrite2.Modellen.Accounts;

public abstract class AccountZakelijk(string email, string wachtwoord, int bedrijfId)
    : Account(email, wachtwoord), IAccountZakelijk
{
    public int BedrijfId { get; set; } = bedrijfId;
    [ForeignKey(nameof(BedrijfId))] public Bedrijf Bedrijf { get; set; }
}