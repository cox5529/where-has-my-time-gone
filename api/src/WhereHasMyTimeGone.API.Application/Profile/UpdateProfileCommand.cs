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

namespace WhereHasMyTimeGone.API.Application.Profile;

public class UpdateProfileCommand : IRequest
{
}

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
{
    private readonly IDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateProfileCommandHandler(IDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancel)
    {
        if (string.IsNullOrEmpty(_currentUserService.Email) || string.IsNullOrEmpty(_currentUserService.UserId))
        {
            throw new UnauthorizedException();
        }

        var profile = await _context.Set<UserProfile>().FirstOrDefaultAsync(x => x.UserId == _currentUserService.UserId, cancel)
                   ?? await _context.Set<UserProfile>().FirstOrDefaultAsync(x => x.Email == _currentUserService.Email, cancel)
                   ?? new UserProfile();

        profile.Email = _currentUserService.Email;
        profile.UserId = _currentUserService.UserId;

        _context.Set<UserProfile>().Update(profile);
        await _context.SaveChangesAsync(cancel);

        return Unit.Value;
    }
}

public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator()
    {
    }
}
