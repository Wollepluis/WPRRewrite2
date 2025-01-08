using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPRRewrite2.Interfaces;
using WPRRewrite2.Modellen.Kar;

namespace WPRRewrite2.Controllers;

[ApiController]
[Route("api/Voertuig")]
public class VoertuigController : ControllerBase
{
    private readonly Context _context;

    public VoertuigController(Context context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("AlleVoertuigen")]
    public async Task<ActionResult<IEnumerable<IVoertuig>>> GetAll([FromQuery] DateTime? startDatum, [FromQuery] DateTime? eindDatum, [FromQuery] int? accountId)
    {
        IQueryable<Voertuig> query = _context.Voertuigen;
    
        if (startDatum.HasValue && eindDatum.HasValue)
        {
            var start = DateOnly.FromDateTime(startDatum.Value);
            var eind = DateOnly.FromDateTime(eindDatum.Value);
        
            query = query
                .Include(v => v.Reserveringen)
                .Where(v => !v.Reserveringen.Any() || 
                            !v.Reserveringen.Any(r => 
                                r.Begindatum <= eind && 
                                r.Einddatum >= start));
        }

        if (accountId.HasValue)
        {
            query = query.Where(v => v.AccountId == accountId.Value);
        }

        var voertuigen = await query.ToListAsync();
    
        if (!voertuigen.Any())
        {
            return NotFound(new { Message = "Er staan geen voertuigen in de database" });
        }

        return Ok(new { Voertuigen = voertuigen });
    }

    [HttpGet("SpecifiekVoertuig")]
    public async Task<ActionResult<IVoertuig>> GetSpecific([FromQuery] int id)
    {
        var voertuig = await _context.Voertuigen.FindAsync(id);
        if (voertuig == null)
            return NotFound(new { Message = $"Voertuig met ID {id} staat niet in de database" });

        return Ok(new { voertuig });
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var voertuig = await _context.Voertuigen.FindAsync(id);
        if (voertuig == null)
            return NotFound(new { Message = $"Voertuig met ID {id} staat niet in de database" });

        _context.Voertuigen.Remove(voertuig);
        await _context.SaveChangesAsync();

        return Ok(new { Message = $"Voertuig {voertuig.Merk} {voertuig.Model} succesvol verwijderd" });
    }
}