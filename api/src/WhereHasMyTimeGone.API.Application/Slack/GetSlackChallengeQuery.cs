using WhereHasMyTimeGone.API.Application.Common.Models;

namespace WhereHasMyTimeGone.API.Application.Slack;

using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

public class GetSlackChallengeQuery : IRequest<GetSlackChallengeQueryResponse>, ISlackEvent
{
    public string? Challenge { get; set; }

    public string? Token { get; set; }

    public string? Type { get; set; }
}

public class GetSlackChallengeQueryResponse
{
    public string? Challenge { get; set; }
}

public class GetSlackChallengeQueryHandler : IRequestHandler<GetSlackChallengeQuery, GetSlackChallengeQueryResponse>
{
    public Task<GetSlackChallengeQueryResponse> Handle(GetSlackChallengeQuery request, CancellationToken cancel)
    {
        return Task.FromResult(new GetSlackChallengeQueryResponse { Challenge = request.Challenge });
    }
}
