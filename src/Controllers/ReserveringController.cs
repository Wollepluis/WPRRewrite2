using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPRRewrite2.DTOs;
using WPRRewrite2.Modellen;

namespace WPRRewrite2.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ReserveringController(Context context) : ControllerBase
{
    private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<Reservering>>> GetAll([FromQuery] int? accountId)
    {
        IQueryable<Reservering> query = _context.Reserveringen
            .Include(r => r.Voertuig);

        if (accountId.HasValue)
        {
            query = query.Where(r => r.AccountId == accountId);
        }

        var reserveringen = await query.ToListAsync();

        if (reserveringen.Count == 0)
            return NotFound(new { Message = "Er staan geen reserveringen in de database" });

        return Ok(reserveringen);
    }

    [HttpGet("GetSpecific")]
    public async Task<ActionResult<IEnumerable<Reservering>>> GetAccountReserveringen([FromQuery] int reserveringId)
    {
        var reservering = await _context.Reserveringen.FindAsync(reserveringId);
        if (reservering == null) 
            return NotFound(new { Message = $"Reservering met ID {reserveringId} staat niet in de database" });

        return Ok(reservering);
    }
    
    // [HttpPost("PostReservering")]
    // public async Task<ActionResult<IEnumerable<Reservering>>> PostReservering([FromBody] ReserveringDto reserveringDto)
    // {
    //     if (reserveringDto == null) return BadRequest();
    //
    //     // methode maak reservering
    //     
    //     // _context.Reserveringen.Add(reservering);
    //     // await _context.SaveChangesAsync();
    //     //
    //      return Ok(reservering);
    // }
    
    //put reservering

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] int reserveringId)
    {
        var reservering = await _context.Reserveringen.FindAsync(reserveringId);
        if (reservering == null) 
            return NotFound(new { Message = $"Reservering met ID {reserveringId} staat niet in de database" });

        _context.Reserveringen.Remove(reservering);
        await _context.SaveChangesAsync();

        return Ok(new { Message = $"Reservering succesvol verwijderd" });
    }
    
    // Reserveer Voertuig
}