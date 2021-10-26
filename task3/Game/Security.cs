using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Game
{
    public class Security : IDisposable
    {
        public string HMAC => Convert.ToBase64String(hmac.Hash);
        public string HMACKey => Convert.ToBase64String(hmac.Key);
        public int Move { get; private set; }

        private readonly HMACSHA384 hmac;
        private readonly byte[] secretKey;

        public Security(int maxInt)
        {
            secretKey = new byte[16];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(secretKey);
            Move = RandomNumberGenerator.GetInt32(0, maxInt);
            hmac = new HMACSHA384(secretKey);
            hmac.ComputeHash(BitConverter.GetBytes(Move));
        }

        public void Dispose()
        {
            ((IDisposable)hmac).Dispose();
        }
    }
}
