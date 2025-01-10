namespace WPRRewrite2.DTOs;

public class ReserveringDto (DateOnly begindatum, DateOnly einddatum, int voertuigId, int accountId
    , string voertuigStatus)
{
    public DateOnly Begindatum { get; set; } = begindatum;
    public DateOnly Einddatum { get; set; } = einddatum;
    public int VoertuigId { get; set; } = voertuigId;
    public int AccountId { get; set; } = accountId;
    public string VoertuigStatus { get; set; } = voertuigStatus;
}