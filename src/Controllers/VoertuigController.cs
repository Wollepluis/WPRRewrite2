using Microsoft.AspNetCore.Mvc;

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
    
    []
}