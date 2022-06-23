using Microsoft.EntityFrameworkCore;
using WhereHasMyTimeGone.API.Application.Common.Exceptions;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;
using WhereHasMyTimeGone.API.Domain.Entities;

namespace WhereHasMyTimeGone.API.Application.Usage;

using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

public class FetchPeopleQuery : IRequest<IEnumerable<FetchPeopleQueryResponse>>
{
}

public class FetchPeopleQueryResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public IEnumerable<FetchPeopleQueryResponseUsage> Usages { get; set; } = new List<FetchPeopleQueryResponseUsage>();
}

public class FetchPeopleQueryResponseUsage
{
    public int Id { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public double DurationSeconds { get; set; }
}

public class FetchPeopleQueryHandler : IRequestHandler<FetchPeopleQuery, IEnumerable<FetchPeopleQueryResponse>>
{
    private readonly ICurrentProfileService _currentProfileService;
    private readonly IDbContext _context;

    public FetchPeopleQueryHandler(ICurrentProfileService currentProfileService, IDbContext context)
    {
        _currentProfileService = currentProfileService;
        _context = context;
    }

    public async Task<IEnumerable<FetchPeopleQueryResponse>> Handle(FetchPeopleQuery request, CancellationToken cancel)
    {
        var profile = await _currentProfileService.GetCurrentProfileAsync(cancel);
        if (profile == null)
        {
            throw new ForbiddenAccessException();
        }
        
        var people = await _context.Set<Person>()
                                   .Include(x => x.Usages)
                                   .Where(x => x.UserProfileId == profile.Id)
                                   .Select(
                                        x => new FetchPeopleQueryResponse
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            Usages = x.Usages.Select(
                                                u => new FetchPeopleQueryResponseUsage
                                                {
                                                    Id = u.Id,
                                                    Start = u.Start,
                                                    End = u.End,
                                                    DurationSeconds = u.Duration.TotalSeconds
                                                })
                                        })
                                   .ToListAsync(cancel);

        return people;
    }
}

public class FetchPeopleQueryValidator : AbstractValidator<FetchPeopleQuery>
{
    public FetchPeopleQueryValidator()
    {
    }
}
