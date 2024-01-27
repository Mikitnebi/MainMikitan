using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.InternalServiceAdapter.Hasher
{
    public class PasswordHasher(IOptions<SecurityOptions> securityOptions) : IPasswordHasher
    {
        private readonly SecurityOptions _securityOptions = securityOptions.Value;

        public string HashPassword(string password)
        {
            var key = _securityOptions.Key;
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            // Compute the hash of the password bytes
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashedPasswordBytes = hmac.ComputeHash(passwordBytes);

            // Convert the hash bytes to a hexadecimal string representation
            var hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "");

            return hashedPassword;
        }

        // Function to verify a password against a hashed password with the same key
        public bool VerifyPassword(string password, string hashedPassword)
        {
            var newHashedPassword = HashPassword(password);
            var test=newHashedPassword.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
            return test;
        }
    }
}
