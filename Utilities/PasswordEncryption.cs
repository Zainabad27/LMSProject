using BCrypt.Net;

namespace LmsApp2.Api.Utilities
{
    public static class PasswordEncryption
    {

        public static string GetHashedPassword(this string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);


        }
        



        public static bool VerifyHashedPassword(this string password,string hash) {

            return BCrypt.Net.BCrypt.Verify(password, hash);


        }
    }
}
