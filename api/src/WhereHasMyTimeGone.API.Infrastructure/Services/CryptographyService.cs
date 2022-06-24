using System.Security.Cryptography;
using System.Text;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;

namespace WhereHasMyTimeGone.API.Infrastructure.Services;

public class CryptographyService : ICryptographyService
{
    public string GenerateHmacSha256Signature(string stringToSign, string key)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var plaintextBytes = Encoding.UTF8.GetBytes(stringToSign);

        var hash = new HMACSHA256(keyBytes);
        var signature = hash.ComputeHash(plaintextBytes);
        return Convert.ToHexString(signature);
    }
}
