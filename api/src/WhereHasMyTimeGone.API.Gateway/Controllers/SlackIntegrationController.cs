using MediatR;
using Microsoft.AspNetCore.Mvc;
using WhereHasMyTimeGone.API.Application.Slack;
using WhereHasMyTimeGone.API.Gateway.Attributes;

namespace WhereHasMyTimeGone.API.Gateway.Controllers;

[ApiController]
[ValidateSlackRequest]
[Route("api/slack-integration")]
public class SlackIntegrationController : Controller
{
    private readonly ISender _sender;

    public SlackIntegrationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyUrl(GetSlackChallengeQuery request, CancellationToken cancel = default)
    {
        var result = await _sender.Send(request, cancel);
        return Ok(result);
    }
}
