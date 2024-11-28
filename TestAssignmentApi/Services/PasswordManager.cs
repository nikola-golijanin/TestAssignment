using System.Security.Cryptography;
using System.Text;

namespace TestAssignmentApi.Services;

public static class PasswordManager
{
    private const int _keySizeInBytes = 64;
    private const int _iterations = 350000;
    private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

    public static (string hash, string salt) HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(_keySizeInBytes);
        var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, _iterations, _hashAlgorithm, _keySizeInBytes);
        return (Convert.ToHexString(hash),
                Convert.ToBase64String(salt));
    }

    public static bool VerifyPassword(string password, string hash, string saltString)
    {
        var salt = Convert.FromBase64String(saltString);
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _hashAlgorithm, _keySizeInBytes);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}
