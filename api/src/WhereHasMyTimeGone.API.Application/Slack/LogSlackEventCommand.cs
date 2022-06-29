using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;
using WhereHasMyTimeGone.API.Domain.Entities;

namespace WhereHasMyTimeGone.API.Application.Slack;

public class LogSlackEventCommand : IRequest
{
    public string? Type { get; set; }

    public string? Content { get; set; }
}

public class LogSlackEventCommandHandler : IRequestHandler<LogSlackEventCommand>
{
    private readonly IDbContext _context;

    public LogSlackEventCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(LogSlackEventCommand request, CancellationToken cancel)
    {
        await _context.Set<SlackEvent>()
       .AddAsync(
            new SlackEvent
            {
                Timestamp = DateTime.UtcNow,
                Type = request.Type!,
                Content = request.Content!
            },
            cancel);

        await _context.SaveChangesAsync(cancel);

        return Unit.Value;
    }
}

public class LogSlackEventCommandValidator : AbstractValidator<LogSlackEventCommand>
{
    public LogSlackEventCommandValidator()
    {
        RuleFor(x => x.Type).NotNull();
        RuleFor(x => x.Content).NotNull();
    }
}
