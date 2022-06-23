using FluentValidation;
using MediatR;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;
using WhereHasMyTimeGone.API.Domain.Entities;

namespace WhereHasMyTimeGone.API.Application.Usage;

public class CreatePersonCommand : IRequest<int>
{
    public string? Name { get; set; }
}

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
{
    private readonly IDbContext _context;
    private readonly ICurrentProfileService _currentProfileService;

    public CreatePersonCommandHandler(ICurrentProfileService currentProfileService, IDbContext context)
    {
        _currentProfileService = currentProfileService;
        _context = context;
    }

    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancel)
    {
        var profile = await _currentProfileService.GetCurrentProfileAsync(cancel);
        var person = new Person
        {
            Name = request.Name!,
            Disabled = false,
            UserProfileId = profile!.Id
        };

        await _context.Set<Person>().AddAsync(person, cancel);
        await _context.SaveChangesAsync(cancel);

        return person.Id;
    }
}

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
