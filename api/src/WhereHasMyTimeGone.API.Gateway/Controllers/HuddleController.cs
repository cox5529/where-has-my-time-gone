using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhereHasMyTimeGone.API.Application.Slack;

namespace WhereHasMyTimeGone.API.Gateway.Controllers;

[Authorize]
[ApiController]
[Route("api/huddle")]
public class HuddleController : Controller
{
    private readonly ISender _sender;

    /// <inheritdoc />
    public HuddleController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(FetchHuddlesQueryResponse), 200)]
    public async Task<IActionResult> FetchHuddlesForDate(DateTime? date, int? timezoneOffset, CancellationToken cancel = default)
    {
        var result = await _sender.Send(
                         new FetchHuddlesQuery
                         {
                             Date = DateOnly.FromDateTime(date ?? DateTime.Now),
                             TimezoneOffset = timezoneOffset ?? (int) TimeZoneInfo.Local.BaseUtcOffset.TotalMinutes
                         },
                         cancel);

        return Ok(result);
    }
}
