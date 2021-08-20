using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KaffeemaschineWPF.Extensions
{
    public static class StringExtension
    {
        public static string HashPassword(this string text)
        {
            using (SHA512Managed sHA512Managed = new SHA512Managed())
            {
                sHA512Managed.Initialize();
                byte[] vs = Encoding.UTF8.GetBytes(text);
                return BitConverter.ToString(sHA512Managed.ComputeHash(vs)).Replace("-", "");
            }
        }
    }
}