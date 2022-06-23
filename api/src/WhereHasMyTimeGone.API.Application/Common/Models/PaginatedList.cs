namespace WhereHasMyTimeGone.API.Application.Common.Models;

public class PaginatedList<T>
{
    public IList<T> Items { get; set; } = new List<T>();

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalItems { get; set; }

    public int TotalPages => (int) Math.Ceiling((TotalItems + 0.0) / PageSize);

    public bool HasPreviousPage => Page > 0;

    public bool HasNextPage => Page < TotalPages - 1;
}
