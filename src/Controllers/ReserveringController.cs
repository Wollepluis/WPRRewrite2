using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPRRewrite2.Modellen;

namespace WPRRewrite2.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ReserveringController(Context context) : ControllerBase
{
    private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<Reservering>>> GetAll([FromQuery] int? id)
    {
        IQueryable<Reservering> query = _context.Reserveringen;

        if (id.HasValue)
        {
            query = query
                .Include(r => r.Voertuig)
                .Where(r => r.AccountId == id);
        }
        
        var reserveringen = await query.ToListAsync();
        
        if (reserveringen.Count == 0)
            return NotFound(new { Message = "Er staan geen reserveringen in de database" });

        return Ok(new { reserveringen });
    }

    [HttpGet("GetSpecific")]
    public async Task<ActionResult<IEnumerable<Reservering>>> GetAccountReserveringen([FromQuery] int id)
    {
        var reservering = await _context.Reserveringen.FindAsync(id);
        if (reservering == null) 
            return NotFound(new { Message = $"Reservering met ID {id} staat niet in de database" });

        return Ok(new { reservering });
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var reservering = await _context.Reserveringen.FindAsync(id);
        if (reservering == null) 
            return NotFound(new { Message = $"Reservering met ID {id} staat niet in de database" });

        _context.Reserveringen.Remove(reservering);
        await _context.SaveChangesAsync();

        return Ok(new { Message = $"Reservering succesvol verwijderd" });
    }
    //Reservering CRUD
    
    // Reserveer Voertuig
}