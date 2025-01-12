using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPRRewrite2.DTOs;
using WPRRewrite2.Interfaces;
using WPRRewrite2.Modellen;
using WPRRewrite2.Modellen.Accounts;

namespace WPRRewrite2.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class AccountController(Context context) : ControllerBase
{
    private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<IAccount>>> GetAll([FromQuery] string? accountType, [FromQuery] int? bedrijfId)
    {
        var accounts = await _context.Accounts.ToListAsync();
        if (accounts.Count == 0) 
            return NotFound(new { Message = "Er staan geen accounts in de database" });

        return Ok(new { accounts });
    }

    [HttpGet("GetSpecifiek")]
    public async Task<ActionResult<IAccount>> GetSpecific([FromQuery] int id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null) 
            return NotFound(new { Message = $"Account met ID {id} staat niet in de database"});
        
        return Ok(account.CastAccount(account));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == login.Email);
        if (account == null) 
            return Unauthorized(new { Message = $"Account {login.Email} niet gevonden" });
        
        if (account.VerifyPassword(login.Wachtwoord) == PasswordVerificationResult.Failed) 
            return Unauthorized(new { Message = "Incorrect wachtwoord" });

        return Ok(account.CastAccount(account));
    }
    
    [HttpPost("Registreer")]
    public async Task<ActionResult<IAccount>> Create([FromBody] AccountDto gegevens)
    {
        var checkEmail = _context.Accounts.Any(a => a.Email == gegevens.Email);
        if (checkEmail) 
            return BadRequest("Een gebruiker met deze Email bestaat al");

        Adres adres;
        Bedrijf bedrijf;
        Account nieuwAccount = null;
        
        switch (gegevens.AccountType)
        {
            case "ZakelijkBeheerder":
            {
                bedrijf = await _context.Bedrijven.FindAsync(gegevens.Nummer);
                if (bedrijf == null)
                    return BadRequest("Ongeldige bedrijfId");

                adres = await _context.Adressen.FindAsync(gegevens.AdresId);
                if (adres == null)
                    return BadRequest("Ongeldige adresId");
                
                nieuwAccount = Account.MaakAccount(gegevens, adres.AdresId, bedrijf.BedrijfId);
                
                break;
            }
            case "Particulier":
            {
                adres = await _context.Adressen.FindAsync(gegevens.AdresId);
                if (adres == null)
                    return BadRequest("Ongeldige adresId");
                
                nieuwAccount = Account.MaakAccount(gegevens, adres.AdresId, 0);
                
                break;
            }
        }
    
        _context.Accounts.Add(nieuwAccount);
        await _context.SaveChangesAsync();
        
        EmailSender.VerstuurBevestigingEmail(nieuwAccount.Email);

        var castedAccount = nieuwAccount.CastAccount(nieuwAccount);
        return Ok(new { castedAccount, Message = $"Account {nieuwAccount.Email} is succesvol aangemaakt" });
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromQuery] int id, [FromBody] AccountDto nieuweGegevens)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null)
            return NotFound(new { Message = $"Account met ID {id} staat niet in de database"});
        
        account.UpdateAccount(nieuweGegevens);

        return Ok(new { Message = "Account succesvol aangepast" });
    }
    
    //Zakelijk Huurder toevoegen aan abonnement
    
    //zakelijk huurder verwijderen van abonnement

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null) 
            return NotFound(new { Message = $"Account met ID {id} staat niet in de database" });

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
        
        EmailSender.VerstuurVerwijderEmail(account.Email);

        return Ok(new { Message = $"Account {account.Email} succesvol verwijderd" });
    }
}