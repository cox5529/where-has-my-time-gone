namespace WhereHasMyTimeGone.API.Domain.Common;

public interface IBaseEntity<TKey>
{
    public TKey Id { get; set; }

    public bool Disabled { get; set; }
}
