using System;

namespace Core.Services
{
    public class TokenGeneratorService
    {
        private readonly int size;

        public TokenGeneratorService(int size)
        {
            this.size = size;
        }

        public string Generate()
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            return new string(chars);
        }
    }
}
