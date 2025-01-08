using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPRRewrite2.DTOs;
using WPRRewrite2.Modellen;

namespace WPRRewrite2.Controllers;

[ApiController]
[Route("api/Reservering")]
public class ReserveringController : ControllerBase
{
    private readonly Context _context;

    public ReserveringController(Context context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("AlleReserveringen")]
    public async Task<ActionResult<IEnumerable<Reservering>>> GetAll()
    {
        var reserveringen = await _context.Reserveringen.ToListAsync();
        if (reserveringen.Count == 0)
            return NotFound(new { Message = "Er staan geen reserveringen in de database" });

        return Ok(new { reserveringen });
    }

    [HttpGet("AccountReserveringen")]
    public async Task<ActionResult<IEnumerable<Reservering>>> GetAccountReserveringen([FromQuery] int id)
    {
        var reserveringen = await _context.Reserveringen.Where(r => r.AccountId == id).ToListAsync();
        if (reserveringen.Count == 0) 
            return NotFound(new { Message = "Dit account heeft nog geen reserveringen" });

        return Ok(new { reserveringen });
    }
    
    [HttpPost("ReserveerVoertuig")]
    public async Task<IActionResult> ReserveerVoertuig([FromBody] ReserveringDto reservering)
    {
        
    }
}