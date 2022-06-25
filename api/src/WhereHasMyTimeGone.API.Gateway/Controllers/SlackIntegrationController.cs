using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WhereHasMyTimeGone.API.Application.Common.Models;
using WhereHasMyTimeGone.API.Application.Slack;
using WhereHasMyTimeGone.API.Gateway.Attributes;

namespace WhereHasMyTimeGone.API.Gateway.Controllers;

[ApiController]
[ValidateSlackRequest]
[Route("api/slack-integration")]
public class SlackIntegrationController : Controller
{
    private readonly ISender _sender;
    private readonly ILogger<SlackIntegrationController> _logger;

    public SlackIntegrationController(ISender sender, ILogger<SlackIntegrationController> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    [HttpPost("")]
    public async Task<IActionResult> Event(SlackEvent request, CancellationToken cancel = default)
    {
        if (request.Type == "url_verification")
        {
            var result = await ProcessEvent<GetSlackChallengeQuery, GetSlackChallengeQueryResponse>(cancel);
            return Ok(result);
        }

        return NotFound();
    }

    private async Task<TResponse> ProcessEvent<TRequest, TResponse>(CancellationToken cancel = default)
        where TRequest : SlackEvent, IRequest<TResponse>
    {
        var json = await new StreamReader(Request.Body).ReadToEndAsync();
        var body = JsonConvert.DeserializeObject<TRequest>(json);
        
        _logger.LogCritical(json);

        return await _sender.Send(body, cancel);
    }
}
