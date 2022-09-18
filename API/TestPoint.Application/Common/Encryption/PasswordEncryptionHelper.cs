using System.Security.Cryptography;
using System.Text;

namespace TestPoint.Application.Common.Encryption;

internal static class PasswordEncryptionHelper
{
    public static string ComputeHash(string password)
    {
        using var hmac = new HMACSHA256();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(Encoding.Unicode.GetBytes(password));

        var part1 = Convert.ToBase64String(salt);
        var part2 = Convert.ToBase64String(hash);
        return part1 + part2;
    }

    public static bool VerifyPassword(string password, string hash)
    {
        var salt = Convert.FromBase64String(hash[..88]);
        var databaseHash = Convert.FromBase64String(hash[88..]);

        using var hmac = new HMACSHA256(salt);
        var inputHash = hmac.ComputeHash(Encoding.Unicode.GetBytes(password));
        return inputHash.SequenceEqual(databaseHash);
    }
}