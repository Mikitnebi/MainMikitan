﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Common.OtpGenerator
{
    public class OtpGenerator
    {
        //ოტპ გენერატორი
       public static string OtpGenerate(int n = 6)
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