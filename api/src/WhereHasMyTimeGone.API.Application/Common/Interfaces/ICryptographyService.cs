namespace WhereHasMyTimeGone.API.Application.Common.Interfaces;

public interface ICryptographyService
{
    string GenerateHmacSha256Signature(string stringToSign, string key);
}
