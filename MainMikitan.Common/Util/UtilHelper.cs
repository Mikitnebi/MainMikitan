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
        public static string GenerateUserName()
        {
            string userName = string.Empty;
            string bigAlpha = "QWERTYUIOPLKJHGFDSAZXCVBNM";
            string smallAlpha = bigAlpha.ToLower();
            string numeric = "1234567890";
            Random random = new Random();
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
            string password = string.Empty;
            string bigAlpha = "QWERTYUIOPLKJHGFDSAZXCVBNM";
            string smallAlpha = bigAlpha.ToLower();
            string numeric = "1234567890";
            string character = "!@#$%&*()";
            string strongPassword = $"{bigAlpha}{smallAlpha}{numeric}{character}";
            Random random = new Random();
            for(int i = 0; i < n; i++)
            {
                password += strongPassword[random.Next(strongPassword.Length)];
            }
            return password;
        }
    }
}
