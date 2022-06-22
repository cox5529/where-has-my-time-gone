namespace WhereHasMyTimeGone.API.Application.Common.Models;

public class CapRequest
{
    public string UserId { get; set; }
    
    public string Email { get; set; }
}

public class CapRequest<T> : CapRequest
{
    public T Body { get; set; }
}
