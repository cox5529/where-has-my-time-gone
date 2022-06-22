namespace WhereHasMyTimeGone.API.Application.Common.Interfaces;

public interface ICapService
{
    Task PublishAsync<T>(string name, T data, CancellationToken cancel = default);
}
