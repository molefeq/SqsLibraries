using System;
using System.Security.Cryptography;
using System.Text;

namespace SqsLibraries.Common.Utilities
{
    public class GeneratePassword
    {
        public static byte[] PasswordSalt()
        {
            UnicodeEncoding utf16 = new UnicodeEncoding();
            Random random = new Random(unchecked((int)DateTime.Now.Ticks));

            if (random != null)
            {
                // Create an array of random values.

                byte[] saltValue = new byte[16];

                random.NextBytes(saltValue);

                return saltValue;
            }

            return null;
        }

        public static byte[] HashedPassword(string password, byte[] passwordSalt)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            HashAlgorithm hashAlgorithm = new SHA256Managed();

            if (!string.IsNullOrEmpty(password) && hashAlgorithm != null && encoding != null)
            {
                byte[] binaryPassword = encoding.GetBytes(password);
                byte[] valueToHash = new byte[passwordSalt.Length + binaryPassword.Length];

                // Copy the salt value and the password to the hash buffer.

                passwordSalt.CopyTo(valueToHash, 0);
                binaryPassword.CopyTo(valueToHash, 16);

                byte[] hashValue = hashAlgorithm.ComputeHash(valueToHash);

                return hashValue;
            }

            return null;
        }

        public static string CreateRandomPassword()
        {
            int passwordLength = 8;
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
