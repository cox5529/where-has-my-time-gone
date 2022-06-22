using WhereHasMyTimeGone.API.Domain.Common;

namespace WhereHasMyTimeGone.API.Domain.Entities;

public class UserProfile : IBaseEntity<Guid>
{
    public Guid Id { get; set; }

    public bool Disabled { get; set; }

    public string UserId { get; set; }

    public string Email { get; set; }
}
