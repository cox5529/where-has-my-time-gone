using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;
using WhereHasMyTimeGone.API.Application.Common.Models;
using WhereHasMyTimeGone.API.Application.Common.Models.Slack;
using WhereHasMyTimeGone.API.Application.Common.Settings;
using WhereHasMyTimeGone.API.Domain.Entities;

namespace WhereHasMyTimeGone.API.Application.Slack;

public class InsertHuddleStateCommand : SlackEvent<SlackHuddleUpdateEvent>, IRequest
{
}

public class InsertHuddleStateCommandHandler : IRequestHandler<InsertHuddleStateCommand>
{
    private readonly ILogger<InsertHuddleStateCommandHandler> _logger;
    private readonly IDbContext _context;

    public InsertHuddleStateCommandHandler(ILogger<InsertHuddleStateCommandHandler> logger, IDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<Unit> Handle(InsertHuddleStateCommand request, CancellationToken cancel)
    {
        var time = DateTimeOffset.FromUnixTimeSeconds(request.EventTime).UtcDateTime;
        var email = request.Event!.User!.Profile!.Email!;
        var lastHuddle = await _context.Set<Huddle>()
                                       .Include(x => x.UserProfile)
                                       .Where(x => x.End == null && x.UserProfile!.Email == email)
                                       .OrderByDescending(x => x.Start)
                                       .FirstOrDefaultAsync(cancel);

        if (request.Event!.User!.Profile!.HuddleState == "default_unset")
        {
            if (lastHuddle == null)
            {
                return Unit.Value;
            }

            lastHuddle.UserProfile!.ProfileImage = request.Event.User.Profile.Image512;
            lastHuddle.UserProfile!.DisplayName = request.Event.User.Profile.RealName;
            lastHuddle.End = time;
            _logger.LogInformation($"Ending huddle for {email}.");
        }
        else if (lastHuddle == null)
        {
            var huddle = new Huddle
            {
                Start = time,
            };

            var profile = await _context.Set<UserProfile>().FirstOrDefaultAsync(x => x.Email == email, cancel);
            if (profile == null)
            {
                profile = new UserProfile
                {
                    Email = email,
                    ProfileImage = request.Event.User.Profile.Image512,
                    Disabled = false,
                    Id = Guid.NewGuid()
                };

                _logger.LogInformation($"Creating shadow profile for {email}.");
                await _context.Set<UserProfile>().AddAsync(profile, cancel);
            }

            _logger.LogInformation($"Starting huddle for {email}.");
            huddle.UserProfileId = profile.Id;
            await _context.Set<Huddle>().AddAsync(huddle, cancel);
        }

        await _context.SaveChangesAsync(cancel);
        return Unit.Value;
    }
}

public class InsertHuddleStateCommandValidator : AbstractValidator<InsertHuddleStateCommand>
{
    public InsertHuddleStateCommandValidator(IOptions<SlackSettings> settings)
    {
        RuleFor(x => x.Event)
           .Must(x => x?.User?.Profile?.Email != null)
           .WithMessage("Event must be fully-populated.")
           .Must(x => x?.User?.TeamId == settings.Value.TeamId)
           .WithMessage("This request is not for the correct team.");
    }
}
