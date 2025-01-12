using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPRRewrite2.DTOs;
using WPRRewrite2.Modellen;

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
    public async Task<ActionResult<Bedrijf>> GetSpecific([FromQuery] int id)
    {
        var bedrijf = await _context.Adressen.FindAsync(id);
        if (bedrijf == null)
            return NotFound(new { Message = $"Bedrijf met ID {id} staat niet in de database" });

        return Ok(bedrijf);
    }
    
    // Get Bedrijfstatistieken

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