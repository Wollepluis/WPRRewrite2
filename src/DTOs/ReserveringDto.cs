namespace WPRRewrite2.DTOs;

public class ReserveringDto ()
{
    public DateOnly Begindatum { get; set; }
    public DateOnly Einddatum { get; set; }
    public int VoertuigId { get; set; }
    public int AccountId { get; set; }
    public string VoertuigStatus { get; set; }
}