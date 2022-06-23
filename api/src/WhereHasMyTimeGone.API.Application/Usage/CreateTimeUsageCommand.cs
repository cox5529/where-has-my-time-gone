using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WhereHasMyTimeGone.API.Application.Common.Exceptions;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;
using WhereHasMyTimeGone.API.Domain.Entities;
using WhereHasMyTimeGone.API.Domain.Enums;

namespace WhereHasMyTimeGone.API.Application.Usage;

public class CreateTimeUsageCommand : IRequest<int>
{
    public int PersonId { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }
}

public class CreateTimeUsageCommandHandler : IRequestHandler<CreateTimeUsageCommand, int>
{
    private readonly IDbContext _context;
    private readonly ICurrentProfileService _currentProfileService;

    public CreateTimeUsageCommandHandler(ICurrentProfileService currentProfileService, IDbContext context)
    {
        _currentProfileService = currentProfileService;
        _context = context;
    }

    public async Task<int> Handle(CreateTimeUsageCommand request, CancellationToken cancel)
    {
        var profile = await _currentProfileService.GetCurrentProfileAsync(cancel);
        var person = await _context.Set<Person>().Include(x => x.Usages).FirstOrDefaultAsync(x => x.Id == request.PersonId, cancel);
        if (person == null || person.UserProfileId != profile!.Id)
        {
            throw new NotFoundException();
        }

        var usage = new TimeUsage
        {
            Start = request.Start,
            End = request.End,
            Type = TimeUsageType.Manual
        };
        person.Usages.Add(usage);

        await _context.SaveChangesAsync(cancel);

        return usage.Id;
    }
}

public class CreateTimeUsageCommandValidator : AbstractValidator<CreateTimeUsageCommand>
{
    public CreateTimeUsageCommandValidator()
    {
        RuleFor(x => x.End).GreaterThan(x => x.Start);
    }
}
