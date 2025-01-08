using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPRRewrite2.DTOs;
using WPRRewrite2.Modellen;

namespace WPRRewrite2.Controllers;

[ApiController]
[Route("api/Adres")]
public class AdresController : ControllerBase
{
    private readonly Context _context;

    public AdresController(Context context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("AlleAdressen")]
    public async Task<ActionResult<IEnumerable<Adres>>> GetAll()
    {
        var adressen = await _context.Adressen.ToListAsync();
        if (adressen.Any()) 
            return NotFound(new { Message = "Er staan geen adressen in de database" });

        return Ok(new { adressen });
    }

    [HttpGet("SpecifiekAdres")]
    public async Task<ActionResult<Adres>> GetSpecific([FromQuery] int id)
    {
        var adres = await _context.Adressen.FindAsync(id);
        if (adres == null) 
            return NotFound(new { Message = $"Adres met ID {id} staat niet in de database"});

        return Ok(new { adres });
    }

    [HttpPost("ZoekAdres")]
    public async Task<ActionResult<Adres>> Create([FromBody] AdresDto gegevens)
    {
        var adres = await _context.Adressen
            .Where(a => a.Postcode == gegevens.Postcode && a.Huisnummer == gegevens.Huisnummer).FirstOrDefaultAsync();
        if (adres == null)
        {
            adres = await Adres.ZoekAdres(gegevens.Postcode, gegevens.Huisnummer);
            if (adres == null) return BadRequest(new { Message = "Adres van ingevulde gegevens bestaat niet" });
        }

        _context.Adressen.Add(adres);
        await _context.SaveChangesAsync();

        return Ok(new { adres.AdresId });
    }
    
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var adres = await _context.Adressen.FindAsync(id);
        if (adres == null) 
            return NotFound(new { Message = $"Adres met ID {id} staat niet in de database" });

        _context.Adressen.Remove(adres);
        await _context.SaveChangesAsync();

        return Ok(new { Message = $"Adres succesvol verwijderd" });
    }
}