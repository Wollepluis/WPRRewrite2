using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPRRewrite2.DTOs;
using WPRRewrite2.Modellen;
using WPRRewrite2.Modellen.Accounts;

namespace WPRRewrite2.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class BedrijfController(Context context) : ControllerBase
{
    private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<Bedrijf>>> GetAll()
    {
        var bedrijven = await _context.Bedrijven.ToListAsync();
        if (bedrijven.Count == 0) 
            return NotFound(new { Message = "Er staan geen bedrijven in de database" });

        return Ok(bedrijven);
    }

    [HttpGet("GetSpecific")]
    public async Task<ActionResult<Bedrijf>> GetSpecific([FromQuery] int bedrijfId)
    {
        var bedrijf = await _context.Bedrijven.FindAsync(bedrijfId);
        if (bedrijf == null)
            return NotFound(new { Message = $"Bedrijf met ID {bedrijfId} staat niet in de database" });

        return Ok(bedrijf);
    }
    
    // Get Bedrijfstatistieken

    [HttpDelete("VerwijderBedrijf")]
    public async Task<IActionResult> DeleteBedrijf(int accountId)
    {
        try
        {
            var zakelijkBeheerder = await _context.Accounts
                .OfType<AccountZakelijkBeheerder>()
                .FirstOrDefaultAsync(a => a.AccountId == accountId);
            
            if (zakelijkBeheerder == null)
                return NotFound("Zakelijk beheerder niet gevonden.");

            var bedrijf = await _context.Bedrijven
                .FindAsync(zakelijkBeheerder.BedrijfId);
            
            if (bedrijf == null)
                return NotFound("Er is geen bedrijf gevonden.");

            var abonnement = await _context.Abonnementen
                .FindAsync(accountId);
            
            if (abonnement == null)
                return NotFound("Abonnement niet gevonden.");

            bedrijf.AbonnementId = 0;
            await _context.SaveChangesAsync();

            var bedrijfMetAbonnement = await _context.Bedrijven
                .FirstOrDefaultAsync(b => b.AbonnementId == abonnement.AbonnementId);
            
            if (bedrijfMetAbonnement == null)
            {
                _context.Abonnementen.Remove(abonnement);
                await _context.SaveChangesAsync();
            }

            _context.Bedrijven.Remove(bedrijf);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("U heeft de rechten niet om het account te verwijderen.");
        }
        catch (Exception ex)
        {
            // Log the exception here
            return StatusCode(500, "Er is een fout opgetreden bij het verwijderen van het bedrijf.");
        }
    }
    
    [HttpPost("Create")]
    public async Task<ActionResult<Bedrijf>> Create([FromBody] BedrijfDto bedrijfDto)
    {
        var checkBedrijf = _context.Bedrijven.Any(b => b.KvkNummer == bedrijfDto.KvkNummer);
        if (checkBedrijf)
            return BadRequest(
                new { Message = $"Bedrijf met KvK-Nummer {bedrijfDto.KvkNummer} bestaat al" });

        var nieuwBedrijf = new Bedrijf(bedrijfDto.KvkNummer, bedrijfDto.Bedrijfsnaam, bedrijfDto.Domeinnaam,
            bedrijfDto.BedrijfAdres);

        _context.Bedrijven.Add(nieuwBedrijf);
        await _context.SaveChangesAsync();

        return Ok(new { nieuwBedrijf.BedrijfId });
    }
}