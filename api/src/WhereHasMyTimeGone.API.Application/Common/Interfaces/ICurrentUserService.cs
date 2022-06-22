namespace WhereHasMyTimeGone.API.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string UserId { get; set; }
    string Email { get; set; }
}
