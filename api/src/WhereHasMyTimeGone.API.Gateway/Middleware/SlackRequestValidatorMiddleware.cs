using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WhereHasMyTimeGone.API.Application.Common.Exceptions;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;
using WhereHasMyTimeGone.API.Gateway.Attributes;
using WhereHasMyTimeGone.API.Gateway.Settings;

namespace WhereHasMyTimeGone.API.Gateway.Middleware;

public class SlackRequestValidatorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ICryptographyService _cryptographyService;
    private readonly SlackSettings _settings;
    private readonly ILogger<SlackRequestValidatorMiddleware> _logger;

    public SlackRequestValidatorMiddleware(
        RequestDelegate next,
        ICryptographyService cryptographyService,
        IOptions<SlackSettings> settings,
        ILogger<SlackRequestValidatorMiddleware> logger)
    {
        _next = next;
        _cryptographyService = cryptographyService;
        _logger = logger;
        _settings = settings.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
        var slackRequestAttribute = endpoint?.Metadata.GetMetadata<ValidateSlackRequestAttribute>();
        if (slackRequestAttribute == null)
        {
            await _next(context);
            return;
        }
        

        var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
        var timestampString = context.Request.Headers["X-Slack-Request-Timestamp"].FirstOrDefault() ?? "0";
        var timestamp = long.Parse(timestampString);
        var timestampAsDateTime = DateTime.FromFileTimeUtc(timestamp);
        
        _logger.LogCritical(JsonConvert.SerializeObject(context.Request.Headers));
        _logger.LogCritical(body);

        if (DateTime.UtcNow - timestampAsDateTime > TimeSpan.FromMinutes(5))
        {
            context.Response.StatusCode = 401;
            return;
        }

        var stringToSign = $"v0:{timestampString}:{body}";
        var signature = $"v0={_cryptographyService.GenerateHmacSha256Signature(stringToSign, _settings.SigningKey)}";
        var givenSignature = context.Request.Headers["X-Slack-Signature"];
        if (signature != givenSignature)
        {
            context.Response.StatusCode = 401;
            return;
        }

        await _next(context);
    }
}
