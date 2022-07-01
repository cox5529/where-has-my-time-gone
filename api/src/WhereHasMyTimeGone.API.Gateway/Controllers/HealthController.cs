using Microsoft.AspNetCore.Mvc;

namespace WhereHasMyTimeGone.API.Gateway.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return NoContent();
    }
}
