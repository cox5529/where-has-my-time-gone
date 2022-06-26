using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WhereHasMyTimeGone.API.Application.Common.Models;
using WhereHasMyTimeGone.API.Application.Common.Models.Slack;
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
    public async Task<IActionResult> Event(SlackEventBase request, CancellationToken cancel = default)
    {
        if (request.Type == "url_verification")
        {
            var result = await ProcessEvent<GetSlackChallengeQuery, GetSlackChallengeQueryResponse>(cancel);
            return Ok(result);
        }

        if (request.Type != "event_callback")
        {
            return NotFound();
        }

        var body = await ParseRequestBody<SlackEvent<SlackEventBase>>();
        if (body.Event?.Type != "user_huddle_changed")
        {
            return NotFound();
        }

        await ProcessEvent<InsertHuddleStateCommand, Unit>(cancel);
        return NoContent();

    }

    private async Task<TResponse> ProcessEvent<TRequest, TResponse>(CancellationToken cancel = default)
        where TRequest : SlackEvent, IRequest<TResponse>
    {
        var body = await ParseRequestBody<TRequest>();
        return await _sender.Send(body, cancel);
    }

    private async Task<TRequest> ParseRequestBody<TRequest>()
    {
        Request.Body.Position = 0;
        var json = await new StreamReader(Request.Body).ReadToEndAsync();
        var body = JsonConvert.DeserializeObject<TRequest>(json);
        return body;
    }
}
