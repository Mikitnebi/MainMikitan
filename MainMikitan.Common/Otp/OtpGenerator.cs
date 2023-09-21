using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.InternalServiceAdapter.OtpGenerator
{
    public class OtpGenerator
    {
        //otp generator
       public static string OtpGenerate(int n = 4)
        {
            string otp = "";
            string charsList= "9287634510";
            Random random= new Random();
            for(int i=0; i<n; i++)
            {
                otp += charsList[random.Next(charsList.Length)];
            }
            return otp;
        }
    }
}
