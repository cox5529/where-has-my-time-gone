using WhereHasMyTimeGone.API.Application.Common.Interfaces;

namespace WhereHasMyTimeGone.API.Infrastructure.Services;

public class StringNormalizer : IStringNormalizer
{
    public string Normalize(string s)
    {
        return s.ToUpperInvariant();
    }
}
