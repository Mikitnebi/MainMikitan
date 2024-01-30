using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MainMikitan.InternalServicesAdapter.Util
{
    public class UtilHelper
    {
        public static string GenerateCode()
        {
            var code = string.Empty;
            var n = 6;
            var bigAlpha = "QWERTYUIOPLKJHGFDSAZXCVBNM";
            var smallAlpha = bigAlpha.ToLower();
            var numeric = "1234567890";
            var character = "!@#$%&*()";
            var strongPassword = $"{bigAlpha}{smallAlpha}{numeric}{character}";
            var random = new Random();
            for(var i = 0; i < n; i++)
                code += strongPassword[random.Next(strongPassword.Length)];
            return code;
        }
        public static string GenerateUserName()
        {
            var userName = string.Empty;
            var bigAlpha = "QWERTYUIOPLKJHGFDSAZXCVBNM";
            var smallAlpha = bigAlpha.ToLower();
            var numeric = "1234567890";
            var random = new Random();
            userName += bigAlpha[random.Next(bigAlpha.Length)];
            userName += bigAlpha[random.Next(bigAlpha.Length)];
            userName += smallAlpha[random.Next(smallAlpha.Length)];
            userName += smallAlpha[random.Next(smallAlpha.Length)];
            userName += numeric[random.Next(numeric.Length)];
            userName += numeric[random.Next(numeric.Length)];
            return userName;

        }
        public static string GeneratePassword(int n = 8)
        {
            var password = string.Empty;
            var bigAlpha = "QWERTYUIOPLKJHGFDSAZXCVBNM";
            var smallAlpha = bigAlpha.ToLower();
            var numeric = "1234567890";
            var character = "!@#$%&*()";
            var strongPassword = $"{bigAlpha}{smallAlpha}{numeric}{character}";
            var random = new Random();
            for(var i = 0; i < n; i++)
            {
                password += strongPassword[random.Next(strongPassword.Length)];
            }
            return password;
        }
    }
}
