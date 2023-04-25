using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Ostral.Core.Utilities
{
    internal class Rfc6238AuthenticationProvider
    {
        private static readonly DateTime _unixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly TimeSpan _timestep = TimeSpan.FromMinutes(3);
        private static readonly Encoding _encoding = new UTF8Encoding(false, true);

        private static int ComputeToOneTimePassword(HashAlgorithm hashAlgorithm, ulong timestepNumber, string modifier, int numberOfDigits = 6)
        {
            // # of 0's = length of pin
            //const int mod = 1000000;
            var mod = (int)Math.Pow(10, numberOfDigits);

            // We can add an optional modifier
            var timestepAsBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((long)timestepNumber));
            var hash = hashAlgorithm.ComputeHash(ApplyModifier(timestepAsBytes, modifier));

            // Generate DT string
            var offset = hash[^1] & 0xf;
            Debug.Assert(offset + 4 < hash.Length);
            var binaryCode = (hash[offset] & 0x7f) << 24
                             | (hash[offset + 1] & 0xff) << 16
                             | (hash[offset + 2] & 0xff) << 8
                             | (hash[offset + 3] & 0xff);

            var code = binaryCode % mod;
            return code;
        }

        private static byte[] ApplyModifier(byte[] input, string modifier)
        {
            if (string.IsNullOrEmpty(modifier))
                return input;

            var modifierBytes = _encoding.GetBytes(modifier);
            var combined = new byte[checked(input.Length + modifierBytes.Length)];
            Buffer.BlockCopy(input, 0, combined, 0, input.Length);
            Buffer.BlockCopy(modifierBytes, 0, combined, input.Length, modifierBytes.Length);
            return combined;
        }

        private static ulong GetCurrentTimeStepNumber()
        {
            var delta = DateTime.UtcNow - _unixEpoch;
            return (ulong)(delta.Ticks / _timestep.Ticks);
        }

        public static int GenerateCode(SecurityToken securityToken, string modifier = "", int numberOfDigits = 6)
        {
            if (securityToken == null)
            {
                throw new ArgumentNullException("SecurityToken" ?? string.Empty);
            }

            // Allow a variance of no greater than 90 seconds in either direction
            var currentTimeStep = GetCurrentTimeStepNumber();
            using (var hashAlgorithm = new HMACSHA1(securityToken.GetDataNoClone()))
            {
                var code = ComputeToOneTimePassword(hashAlgorithm, currentTimeStep, modifier, numberOfDigits);
                return code;
            }
        }

        public static bool ValidateCode(SecurityToken securityToken, int code, string modifier = "", int numberOfDigits = 6)
        {
            if (securityToken == null)
                throw new ArgumentNullException("SecurityToken" ?? string.Empty);

            // Allow a variance of no greater than 90 seconds in either direction
            var currentTimeStep = GetCurrentTimeStepNumber();
            using var hashAlgorithm = new HMACSHA1(securityToken.GetDataNoClone());
            for (var i = -2; i <= 2; i++)
            {
                var computedToOneTimePassword = ComputeToOneTimePassword(hashAlgorithm, (ulong)((long)currentTimeStep + i), modifier, numberOfDigits);
                if (computedToOneTimePassword == code)
                {
                    return true;
                }
            }

            // No match
            return false;
        }
    }
}