using System.Security.Cryptography;

namespace ToDoData.Helpers;

public class Hasher
{
    private static byte[] GetSalt()
    {
        return RandomNumberGenerator.GetBytes(32);
    }

    public static Tuple<string, string> Hash(string? password)
    {
        byte[] salt = GetSalt();
        byte[] derivedKey = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA512, 64);

        string hash = Convert.ToBase64String(derivedKey);
        string saltHash = Convert.ToBase64String(salt);

        return new Tuple<string, string>(hash, saltHash);
    }

    public static bool VerifyHash(string? password, string? hash, string? salt)
    {
        byte[] saltByte = Convert.FromBase64String(salt);
        byte[] derivedKey = Rfc2898DeriveBytes.Pbkdf2(password, saltByte, 100000, HashAlgorithmName.SHA512, 64);

        return CryptographicOperations.FixedTimeEquals(derivedKey, Convert.FromBase64String(hash));
    }
}
