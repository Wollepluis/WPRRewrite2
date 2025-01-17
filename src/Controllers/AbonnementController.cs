﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPRRewrite2.DTOs;
using WPRRewrite2.Interfaces;
using WPRRewrite2.Modellen.Abbo;

namespace WPRRewrite2.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class AbonnementController(Context context) : ControllerBase
{
    private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<IAbonnement>>> GetAll([FromQuery] int? bedrijfId)
    {
        IQueryable<IAbonnement> query = _context.Abonnementen;
        
        if (bedrijfId.HasValue)
        {
            query = query
                .Where(a => a.BedrijfId == bedrijfId);
        }

        var abonnementen = await query.ToListAsync();

        if (abonnementen.Count == 0)
            return NotFound(new { Message = "Er staan geen abonnementen in de database" });
        
        return Ok(abonnementen);
    }

    [HttpGet("GetSpecific")]
    public async Task<ActionResult<IAbonnement>> GetSpecific([FromQuery] int abonnementId)
    {
        var abonnement = await _context.Abonnementen.FindAsync(abonnementId);
        if (abonnement == null)
            return NotFound(new { Message = $"Abonnement met ID {abonnementId} staat niet in de database" });

        return Ok(abonnement);
    }

    [HttpPost("Create")]
    public async Task<ActionResult<IAbonnement>> Create(AbonnementDto abonnementDto)
    {
        var checkAbonnement = _context.Abonnementen
            .Any(a => a.AbonnementType == abonnementDto.AbonnementType 
                      && a.BedrijfId == abonnementDto.BedrijfId 
                      && a.MaxVoertuigen == abonnementDto.MaxVoertuigen 
                      && a.MaxMedewerkers == abonnementDto.MaxMedewerkers);
        if (checkAbonnement)
            return BadRequest(new { Message = "Een abonnement met deze gegevens is nog steeds geldig" });

        var nieuwAbonnement = Abonnement.MaakAbonnement(abonnementDto);

        _context.Abonnementen.Add(nieuwAbonnement);
        await _context.SaveChangesAsync();

        return Ok(new { nieuwAbonnement.AbonnementId, Message = $"Nieuw {nieuwAbonnement.AbonnementType} abonnement gaat {nieuwAbonnement.BeginDatum} in" });
    }
}