using Microsoft.EntityFrameworkCore;
using WhereHasMyTimeGone.API.Application.Common.Exceptions;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;
using WhereHasMyTimeGone.API.Application.Common.Security;
using WhereHasMyTimeGone.API.Domain.Entities;
using FluentValidation;
using MediatR;

namespace WhereHasMyTimeGone.API.Application.Slack;

[Authorize]
public class FetchHuddlesQuery : IRequest<FetchHuddlesQueryResponse>
{
    public DateOnly? Date { get; set; }

    public int TimezoneOffset { get; set; }
}

public class FetchHuddlesQueryResponse
{
    public DateTime Start { get; set; }

    public IEnumerable<FetchHuddlesQueryProfileResponse> Profiles { get; set; } =
        new List<FetchHuddlesQueryProfileResponse>();
}

public class FetchHuddlesQueryProfileResponse
{
    public Guid UserProfileId { get; set; }

    public string Name { get; set; } = "";

    public string? ProfileImage { get; set; }

    public List<FetchHuddlesQueryHuddleResponse>? Huddles { get; set; }
}

public class FetchHuddlesQueryHuddleResponse
{
    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public IEnumerable<Guid> Groups { get; set; } = new List<Guid>();
}

public class FetchHuddlesQueryHandler : IRequestHandler<FetchHuddlesQuery, FetchHuddlesQueryResponse>
{
    private readonly IDbContext _context;
    private readonly ICurrentProfileService _currentProfileService;

    public FetchHuddlesQueryHandler(IDbContext context, ICurrentProfileService currentProfileService)
    {
        _context = context;
        _currentProfileService = currentProfileService;
    }

    public async Task<FetchHuddlesQueryResponse> Handle(FetchHuddlesQuery request, CancellationToken cancel)
    {
        var profile = await _currentProfileService.GetCurrentProfileAsync(cancel);
        var hasHuddles = await _context.Set<Huddle>().AnyAsync(x => x.UserProfileId == profile!.Id, cancel);
        if (!hasHuddles)
        {
            throw new ForbiddenAccessException();
        }

        var start = request.Date!.Value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)
                    - TimeSpan.FromMinutes(request.TimezoneOffset);
        var end = start + TimeSpan.FromDays(1);

        var huddles = await _context.Set<Huddle>()
            .Include(x => x.HuddleLinks)
            .Where(x => x.Start >= start && x.Start <= end)
            .ToListAsync(cancel);

        var huddleGroups = huddles.GroupBy(x => x.UserProfileId);

        var profiles = await _context.Set<UserProfile>()
            .Where(x => x.Huddles.Any())
            .OrderByDescending(x => x.Huddles.Count)
            .ToListAsync(cancel);

        var profileResponses = profiles.Select(
                x => new FetchHuddlesQueryProfileResponse
                {
                    Name = x.DisplayName ?? "",
                    ProfileImage = x.ProfileImage,
                    UserProfileId = x.Id,
                    Huddles = huddleGroups.FirstOrDefault(g => g.Key == x.Id)
                                  ?.Select(
                                      e => new FetchHuddlesQueryHuddleResponse
                                      {
                                          Start = e.Start,
                                          End = e.End ?? DateTime.UtcNow,
                                          Groups = e.HuddleLinks.Where(l => l.Group!.OwnerId == profile!.Id)
                                              .Select(l => l.GroupId)
                                              .ToList()
                                      })
                                  .ToList()
                              ?? new List<FetchHuddlesQueryHuddleResponse>()
                })
            .ToList();

        return new FetchHuddlesQueryResponse
        {
            Profiles = profileResponses, Start = start
        };
    }
}

public class FetchHuddlesQueryValidator : AbstractValidator<FetchHuddlesQuery>
{
    public FetchHuddlesQueryValidator()
    {
        RuleFor(x => x.Date).NotNull();
        RuleFor(x => x.TimezoneOffset).LessThanOrEqualTo(60 * 12).GreaterThanOrEqualTo(60 * -12);
    }
}