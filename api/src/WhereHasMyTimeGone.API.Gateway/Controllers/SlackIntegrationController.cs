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
        var json = await GetRequestBodyAsStringAsync();
        if (request.Type == "url_verification")
        {
            var result = await ProcessEvent<GetSlackChallengeQuery, GetSlackChallengeQueryResponse>(json, cancel);
            return Ok(result);
        }

        if (request.Type != "event_callback")
        {
            return NotFound();
        }

        var body = ParseRequestBody<SlackEvent<SlackEventBase>>(json);
        if (body.Event?.Type != "user_huddle_changed")
        {
            return NotFound();
        }

        await ProcessEvent<InsertHuddleStateCommand, Unit>(json, cancel);
        return NoContent();
    }

    private async Task<TResponse> ProcessEvent<TRequest, TResponse>(string json, CancellationToken cancel = default)
        where TRequest : SlackEvent, IRequest<TResponse>
    {
        var body = ParseRequestBody<TRequest>(json);
        var response = await _sender.Send(body, cancel);

        await _sender.Send(
            new LogSlackEventCommand
            {
                Type = nameof(TRequest),
                Content = json
            },
            cancel);

        return response;
    }

    private TRequest ParseRequestBody<TRequest>(string? json)
    {
        return JsonConvert.DeserializeObject<TRequest>(json);
    }

    private async Task<string> GetRequestBodyAsStringAsync()
    {
        Request.Body.Position = 0;
        return await new StreamReader(Request.Body).ReadToEndAsync();
    }
}
