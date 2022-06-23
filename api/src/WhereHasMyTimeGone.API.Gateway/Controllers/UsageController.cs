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
    [ProducesResponseType(201)]
    public async Task<IActionResult> CreatePerson(CreatePersonCommand request, CancellationToken cancel = default)
    {
        var id = await _sender.Send(request, cancel);
        return Created($"/api/usage/person/{id}", id);
    }

    [HttpGet("person")]
    [ProducesResponseType(typeof(IEnumerable<FetchPeopleQueryResponse>), 200)]
    public async Task<IActionResult> FetchPeople(CancellationToken cancel = default)
    {
        var people = await _sender.Send(new FetchPeopleQuery(), cancel);
        return Ok(people);
    }

    [HttpPost("time-usage")]
    [ProducesResponseType(201)]
    public async Task<IActionResult> CreateUsage(CreateTimeUsageCommand request, CancellationToken cancel = default)
    {
        var id = await _sender.Send(request, cancel);
        return Created($"/api/usage/usage/{id}", id);
    }
}
