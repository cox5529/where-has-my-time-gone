using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;
using WhereHasMyTimeGone.API.Application.Common.Settings;
using WhereHasMyTimeGone.API.Gateway.Attributes;
using WhereHasMyTimeGone.API.Gateway.Settings;

namespace WhereHasMyTimeGone.API.Gateway.Middleware;

public class SlackRequestValidatorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ICryptographyService _cryptographyService;
    private readonly SlackSettings _settings;
    private readonly IHostEnvironment _environment;

    public SlackRequestValidatorMiddleware(
        RequestDelegate next,
        ICryptographyService cryptographyService,
        IOptions<SlackSettings> settings,
        IHostEnvironment environment)
    {
        _next = next;
        _cryptographyService = cryptographyService;
        _environment = environment;
        _settings = settings.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
        var slackRequestAttribute = endpoint?.Metadata.GetMetadata<ValidateSlackRequestAttribute>();
        if (slackRequestAttribute == null || _environment.IsDevelopment())
        {
            await _next(context);
            return;
        }

        var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0;
        var timestampString = context.Request.Headers["X-Slack-Request-Timestamp"].FirstOrDefault() ?? "0";
        var timestamp = long.Parse(timestampString);
        var timestampAsDateTime = DateTimeOffset.FromUnixTimeSeconds(timestamp);

        if (DateTime.UtcNow - timestampAsDateTime > TimeSpan.FromMinutes(5))
        {
            context.Response.StatusCode = 401;
            return;
        }

        var stringToSign = $"v0:{timestampString}:{body}";
        var signature = $"v0={_cryptographyService.GenerateHmacSha256Signature(stringToSign, _settings.SigningSecret!)}";
        var givenSignature = context.Request.Headers["X-Slack-Signature"].FirstOrDefault() ?? "";
        if (signature != givenSignature)
        {
            context.Response.StatusCode = 401;
            return;
        }

        await _next(context);
    }
}
