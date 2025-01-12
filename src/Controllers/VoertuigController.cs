using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPRRewrite2.DTOs;
using WPRRewrite2.Interfaces;
using WPRRewrite2.Modellen.Kar;

namespace WPRRewrite2.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class VoertuigController(Context context) : ControllerBase
{
    private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<IVoertuig>>> GetAll([FromQuery] DateTime? startDatum, [FromQuery] DateTime? eindDatum)
    {
        IQueryable<Voertuig> query = _context.Voertuigen;
    
        if (startDatum.HasValue && eindDatum.HasValue)
        {
            var start = DateOnly.FromDateTime(startDatum.Value);
            var eind = DateOnly.FromDateTime(eindDatum.Value);
        
            query = query
                .Include(v => v.Reservering)
                .Where(v => v.Reservering.Begindatum <= eind && v.Reservering.Einddatum >= start);
        }

        var voertuigen = await query.ToListAsync();
    
        if (voertuigen.Count == 0)
            return NotFound(new { Message = "Er staan geen voertuigen in de database" });
        

        return Ok(voertuigen);
    }

    [HttpGet("GetSpecific")]
    public async Task<ActionResult<IVoertuig>> GetSpecific([FromQuery] int id)
    {
        var voertuig = await _context.Voertuigen.FindAsync(id);
        if (voertuig == null)
            return NotFound(new { Message = $"Voertuig met ID {id} staat niet in de database" });

        return Ok(voertuig);
    }

    [HttpPost("Create")]
    public async Task<ActionResult<IVoertuig>> Create(VoertuigDto voertuigDto)
    {
        var checkVoertuig = _context.Voertuigen
            .Any(v => v.Merk == voertuigDto.Merk && v.Model == voertuigDto.Model);
        if (checkVoertuig)
            return BadRequest(new { Message = $"De {voertuigDto.Merk} {voertuigDto.Model} staat al in de Database" });
        
        var nieuwVoertuig = Voertuig.MaakVoertuig(voertuigDto);

        _context.Voertuigen.Add(nieuwVoertuig);
        await _context.SaveChangesAsync();

        return Ok(new { nieuwVoertuig.VoertuigId, Message = $"De {nieuwVoertuig.Merk} {nieuwVoertuig.Model} is succesvol aangemaakt" });
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateVoertuig([FromBody] IVoertuig updatedVoertuig)
    {
        var voertuig = await _context.Voertuigen.FindAsync(updatedVoertuig.VoertuigId);
        if (voertuig == null)
            return NotFound(new { Message = $"Het voertuig met ID {updatedVoertuig.VoertuigId} staat niet in de database" });
        
        voertuig.UpdateVoertuig(updatedVoertuig);
        await _context.SaveChangesAsync();

        return Ok(new { Message = $"{voertuig.Merk} {voertuig.Model} succesvol geupdate" });
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