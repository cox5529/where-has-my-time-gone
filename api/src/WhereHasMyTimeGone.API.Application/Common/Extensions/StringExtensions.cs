namespace WhereHasMyTimeGone.API.Application.Common.Extensions;

public static class StringExtensions
{
    public static Guid AsGuid(this string s)
    {
        return Guid.TryParse(s, out var x) ? x : Guid.Empty;
    }

    public static TEnum AsEnum<TEnum>(this string s) where TEnum : struct, Enum
    {
        return Enum.Parse<TEnum>(s);
    }
}
