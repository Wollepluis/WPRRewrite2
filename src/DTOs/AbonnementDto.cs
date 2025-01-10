using System.ComponentModel.DataAnnotations;

namespace WPRRewrite2.DTOs;

public class AbonnementDto(int maxVoertuigen, int maxWerknemers, DateOnly begindatum, string abonnementType
    , int bedrijfId)
{
    public int MaxVoertuigen { get; set; } = maxVoertuigen;
    public int MaxWerknemers { get; set; } = maxWerknemers;
    public DateOnly BeginDatum { get; set; } = begindatum;
    
    [MaxLength(255)]public string AbonnementType { get; set; } = abonnementType;

    public int BedrijfId { get; set; } = bedrijfId;
}