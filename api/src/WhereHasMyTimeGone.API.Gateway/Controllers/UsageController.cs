using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhereHasMyTimeGone.API.Application.Usage;

namespace WhereHasMyTimeGone.API.Gateway.Controllers;

[Authorize]
[ApiController]
[Route("api/usage")]
public class UsageController : Controller
{
    private readonly ISender _sender;

    public UsageController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("person")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CreatePerson(CreatePersonCommand request, CancellationToken cancel = default)
    {
        var id = await _sender.Send(request, cancel);
        return Created($"/api/usage/person/{id}", id);
    }

    [HttpPost("time-usage")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CreateUsage(CreateTimeUsageCommand request, CancellationToken cancel = default)
    {
        var id = await _sender.Send(request, cancel);
        return Created($"/api/usage/usage/{id}", id);
    }
}
