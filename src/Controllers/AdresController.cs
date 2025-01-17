﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPRRewrite2.DTOs;
using WPRRewrite2.Modellen;

namespace WPRRewrite2.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class AdresController(Context context) : ControllerBase
{
    private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<Adres>>> GetAll()
    {
        var adressen = await _context.Adressen.ToListAsync();
        if (adressen.Count == 0) 
            return NotFound(new { Message = "Er staan geen adressen in de database" });

        return Ok(adressen);
    }

    [HttpGet("GetSpecific")]
    public async Task<ActionResult<Adres>> GetSpecific([FromQuery] int adresId)
    {
        var adres = await _context.Adressen.FindAsync(adresId);
        if (adres == null) 
            return NotFound(new { Message = $"Adres met ID {adresId} staat niet in de database"});

        return Ok(adres);
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Adres>> Create([FromBody] AdresDto gegevens)
    {
        var adres = await _context.Adressen
            .Where(a => a.Postcode == gegevens.Postcode && a.Huisnummer == gegevens.Huisnummer).FirstOrDefaultAsync();
        if (adres == null)
        {
            try
            {
                adres = await Adres.ZoekAdres(gegevens.Postcode, gegevens.Huisnummer);
            }
            catch (Exception e)
            {
                return NotFound("Het adres is niet gevonden met de bijbehorende postcode en huisnummer...");
            }
            
            if (adres == null) return NotFound("Het adres is niet gevonden met de bijbehorende postcode en huisnummer...");
        
            _context.Adressen.Add(adres);
            await _context.SaveChangesAsync();
        }

        return Ok(new { adres.AdresId });
    }
    
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] int adresId)
    {
        var adres = await _context.Adressen.FindAsync(adresId);
        if (adres == null) 
            return NotFound(new { Message = $"Adres met ID {adresId} staat niet in de database" });

        _context.Adressen.Remove(adres);
        await _context.SaveChangesAsync();

        return Ok(new { Message = $"Adres succesvol verwijderd" });
    }
}