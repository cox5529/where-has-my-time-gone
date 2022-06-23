using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhereHasMyTimeGone.API.Application.Profile;

namespace WhereHasMyTimeGone.API.Gateway.Controllers;

[Authorize]
[ApiController]
[Route("api/profile")]
public class UserProfileController : Controller
{
    private readonly ISender _sender;

    public UserProfileController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("me")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateCurrentProfile(CancellationToken cancel = default)
    {
        await _sender.Send(new UpdateProfileCommand(), cancel);
        return NoContent();
    }
}
