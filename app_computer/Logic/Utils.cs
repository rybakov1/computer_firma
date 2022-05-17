using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text.RegularExpressions;

namespace app_computer.Logic
{
    public static class Utils
    {
        static byte[] salt = new byte[128 / 8];
        public static string hashingPassword(string pass)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pass,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^((\+7|7|8)+([0-9]){10})$").Success;
        }
    }
}
