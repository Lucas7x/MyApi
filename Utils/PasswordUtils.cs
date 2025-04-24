using Isopoh.Cryptography.Argon2;

namespace MyApi.Utils
{
    public class PasswordUtils
    {
        public static string HashPassword(string password)
        {
            return Argon2.Hash(password);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return Argon2.Verify(hash, password);
        }
    }
}
